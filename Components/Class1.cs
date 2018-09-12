using System;

namespace Components
{
   public class Class1
   {
   }
   public interface IMyComponent
   {
      string Description { get; }
      string ManipulateString(string a, string b);
   }
}
