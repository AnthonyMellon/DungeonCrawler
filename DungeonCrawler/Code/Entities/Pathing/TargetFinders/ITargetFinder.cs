using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.Entities.Pathing.TargetFinders
{
    internal interface ITargetFinder
    {
        public Entity FindTarget(Point currentPosition);
    }
}
