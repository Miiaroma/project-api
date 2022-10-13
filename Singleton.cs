using System;

namespace project_api{
public class Singleton
{
    public int? id_person { get; set; }
    public string? Password { get; set; }
    private static Singleton? instance;

   private Singleton() {}

   public static Singleton Instance
   {
      get 
      {
         if (instance == null)
         {
            instance = new Singleton();
         }
         return instance;
      }
   }
}
}