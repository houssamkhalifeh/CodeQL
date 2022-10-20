using System;
using System.Web;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.RazorPages;


public class AssemblyPathInjectionHandler : PageModel  {
  public void ProcessRequest() {
    string assemblyPath1 = Request.Query["assemblyPath"];
    // BAD: Load assembly based on user input
    var badAssembly = Assembly.LoadFile(assemblyPath1);

    // Method called on loaded assembly. If the user can control the loaded assembly, then this
    // could result in a remote code execution vulnerability
    MethodInfo m = badAssembly.GetType("Config").GetMethod("GetCustomPath");
    Object customPath = m.Invoke(null, null);
    // ...
  }
}
