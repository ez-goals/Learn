﻿<%@ Application Language="C#" %>

<script RunAt="server">

  void Application_Start(object sender, EventArgs e)
  {
    // 在应用程序启动时运行的代码
  }

  void Application_End(object sender, EventArgs e)
  {
    //  在应用程序关闭时运行的代码

  }

  void Application_Error(object sender, EventArgs e)
  {
    // 在出现未处理的错误时运行的代码
    Server.Transfer("~/error-handler.aspx");
  }

  void Session_Start(object sender, EventArgs e)
  {
    // 在新会话启动时运行的代码
  }

  void Session_End(object sender, EventArgs e)
  {
    // 在会话结束时运行的代码。 
    // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
    // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
    // 或 SQLServer，则不引发该事件。
  }

  void Application_BeginRequest(object sender, EventArgs e)
  {
    HttpContext.Current.Items["BeginRequestCounter"] = EZGoal.PreciseCounter.Counter;
    PageEvents.Reset();
  }

  void Application_EndRequest(object sender, EventArgs e)
  {
    long eCounter = EZGoal.PreciseCounter.Counter;
    long bCounter = (long)HttpContext.Current.Items["BeginRequestCounter"];
    decimal timetaken = EZGoal.PreciseCounter.TimeSpan(bCounter, eCounter, EZGoal.TimeUnit.MilliSecond);
    var log = new EZGoal.Log(decimal.Round(timetaken, 3));
    try
    {
      log.Write();
    }
    catch (Exception exception)
    {
      EZGoal.Common.LogException(exception, true);
    }
  }

  void Application_PreSendRequestHeaders(object sender, EventArgs e)
  {
  }

  void Application_PreSendRequestContent(object sender, EventArgs e)
  {
  }
       
</script>
