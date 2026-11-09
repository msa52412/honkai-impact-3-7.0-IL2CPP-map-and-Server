using System;
using System.Linq;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Game;

internal static class StigmataRuneSelection
{
    internal static SelectNewStigmataRuneRsp.Retcode Apply(Stigmata[] items, SelectNewStigmataRuneReq request, Action save)
    {
        var item = items?.FirstOrDefault(x => x.UniqueId == request.UniqueId);
        if (item == null) return SelectNewStigmataRuneRsp.Retcode.StigmataNotExist;
        if (item.WaitSelectRuneGroupLists.Count == 0) return SelectNewStigmataRuneRsp.Retcode.NoNewRune;
        var chosen = request.IsSelect
            ? item.WaitSelectRuneGroupLists.Where(x => x.UniqueId == request.SelectUniqueId).ToArray()
            : Array.Empty<StigmataRuneGroup>();
        // A stale or ambiguous selection must never erase the current affixes.
        if (request.IsSelect && (chosen.Length != 1 || chosen[0].RuneLists.Count == 0 ||
            chosen[0].RuneLists.Count > 2 || item.SlotNum > 2 || chosen[0].RuneLists.Count > item.SlotNum ||
            chosen[0].RuneLists.Any(x => x == null || x.RuneId == 0 || x.StrengthPercent > 100)))
            return SelectNewStigmataRuneRsp.Retcode.SelectEmpty;
        var previous = item.RuneLists.ToArray();
        bool previousIdentified = item.IsAffixIdentify;
        var pending = item.WaitSelectRuneGroupLists.ToArray();
        try
        {
            if (request.IsSelect)
            {
                var replacement = chosen[0].RuneLists.Select(x => new StigmataRune { RuneId=x.RuneId, StrengthPercent=x.StrengthPercent }).ToArray();
                item.RuneLists.Clear();
                item.RuneLists.AddRange(replacement);
            }
            // Identification is a separate wire/storage flag, not inferred by protobuf from RuneLists.
            item.IsAffixIdentify = item.RuneLists.Count > 0;
            item.WaitSelectRuneGroupLists.Clear();
            save();
        }
        catch
        {
            item.RuneLists.Clear(); item.RuneLists.AddRange(previous);
            item.IsAffixIdentify = previousIdentified;
            item.WaitSelectRuneGroupLists.Clear(); item.WaitSelectRuneGroupLists.AddRange(pending);
            return SelectNewStigmataRuneRsp.Retcode.Fail;
        }
        return SelectNewStigmataRuneRsp.Retcode.Succ;
    }
}
