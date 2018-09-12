using System;
using System.Collections.Generic;
using System.Text;
using System.Composition;


namespace Components
{
   //[PartCreationPolicy(CreationPolicy.NonShared)]
   [Export(typeof(DOD.IDataStream<long>))]
   public class HP : DOD.DataStream<long, int>
   {
      [ImportingConstructor]
      public HP() : base(nameof(HP))
      {
         Console.WriteLine(this.Name + " Created");
      }
   }

   //[PartCreationPolicy(CreationPolicy.NonShared)]
   //[Export(typeof(IDataStream<long>))]
   //public class DispName : DataStream<long, int>
   //{
   //   [ImportingConstructor]
   //   public DispName() : base(nameof(DispName))
   //   {
   //      Console.WriteLine(this.Name + " Created");
   //   }
   //}
}
