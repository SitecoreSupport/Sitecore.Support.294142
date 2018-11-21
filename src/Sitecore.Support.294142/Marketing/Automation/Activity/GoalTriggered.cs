using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Sitecore.Framework.Conditions;
using Sitecore.XConnect;
using Sitecore.Xdb.MarketingAutomation.Core.Processing.Plan;

namespace Sitecore.Support.Marketing.Automation.Activity
{
  public class GoalTriggered : Sitecore.Marketing.Automation.Activity.GoalTriggered
  {
    public GoalTriggered(ILogger<GoalTriggered> logger)
      : base(logger)
    {
    }

    protected override bool ShouldMove(IContactProcessingContext context)
    {
      Condition.Requires<IContactProcessingContext>(context, nameof(context)).IsNotNull<IContactProcessingContext>();
      if (context.Interaction == null || this.Goals == null || !this.Goals.Any<Guid>())
      {
        return false;
      }

      EventCollection events = context.Interaction.Events;
      IEnumerable<Guid> first = events != null ? events.OfType<Goal>().Select<Goal, Guid>((Func<Goal, Guid>)(x => x.DefinitionId)) : (IEnumerable<Guid>)null;
      int? nullable1 = first != null ? new int?(first.Intersect<Guid>((IEnumerable<Guid>)this.Goals).Count<Guid>()) : new int?();
      if (!this.MatchAll)
      {
        int? nullable2 = nullable1;
        int num = 0;
        if (nullable2.GetValueOrDefault() <= num)
        {
          return false;
        }

        return nullable2.HasValue;
      }
      int? nullable3 = nullable1;
      int count = this.Goals.Count;
      if (nullable3.GetValueOrDefault() != count)
      {
        return false;
      }

      return nullable3.HasValue;
    }
  }
}
