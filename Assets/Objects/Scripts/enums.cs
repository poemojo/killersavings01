using System;
using System.Collections.Generic;
using System.Linq;

public enum SearchType
{
   Hierarchy, Selection, All
}

public enum Categories
{
   None = 0,

   //Actors
   MainActors = 1,
   QuestActors = 2,
   ExtraActors = 3,

   //Items
   KeyItems = 4,
   TradeItems = 5,
   ToolItems = 6,

   //Props
   ContainerProps = 7,
   FurnitureProps = 8,
   GeneralProps = 9
}

public enum VarsConstants
{
   False = int.MinValue,
   True = int.MaxValue
}
