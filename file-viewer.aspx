﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="file-viewer.aspx.cs" Inherits="FileViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>File Lister</title>
  <style type="text/css">
    html, body { height: 100%; margin: 0; padding: 0; width: 100%; }
    form { padding: 50px 30px; position: relative; }
    #navi { position: fixed; height: 50px; left: 0; right: 0; top: 0; padding: 0 40px; background-color: #CC0; z-index: 1; }
      #navi a { color: #090; display: inline-block; font-family: SimSun, sans-serif; font-size: 20px; font-weight: bold; line-height: 48px; text-decoration: none; }
        #navi a img { height: 22px; vertical-align: text-top; }
    #list { position: relative; z-index: 0; }
    ul { margin: 0; padding: 0; list-style-position: inside; list-style-type: none; width: 100%; }
      ul li { margin: 0; padding: 0; }
        ul li > span { display: block; font-size: 18px; font-weight: bold; line-height: 2.25em; }
        ul li a { border-bottom: 1px solid #ddd; color: #000; display: block; font-size: 16px; height: 2.25em; line-height: 2.25em; text-decoration: none; }
          ul li a:hover { box-shadow: 0px 1px 10px #000; }
          ul li a span { display: block; }
    span.name { float: left; padding-left: 10px; text-align: left; }
    span.time { float: right; text-align: center; width: 240px; }
    span.size { float: right; padding-right: 20px; text-align: right; width: 100px; }
    .clearfix { zoom: 1; }
      .clearfix:after { display: table; clear: both; content: ""; }
  </style>
</head>
<body>
  <form id="_form" runat="server">
    <div id="navi">
      <a href="file-viewer.aspx<%= ParentDirectory() %>" target="_self">
        <img src="image/up.png" alt="up" />回到上一层目录
      </a>
    </div>
    <div id="list">
      <ul>
        <asp:Repeater ID="Repeater_Contents" runat="server">
          <HeaderTemplate>
            <li class="clearfix">
              <span class="name">Name</span>
              <span class="size">Size</span>
              <span class="time">Last Modified</span>
            </li>
          </HeaderTemplate>
          <ItemTemplate>
            <li>
              <a <%# ((Dircontent)Container.DataItem).Link %>>
                <span class="name"><%# ((Dircontent)Container.DataItem).Name %></span>
                <span class="size"><%# ((Dircontent)Container.DataItem).Type == "d" ? " --- " : FileSizeToString(((Dircontent)Container.DataItem).Size) %></span>
                <span class="time"><%# ((Dircontent)Container.DataItem).Time.ToString("yyyy-MM-dd HH:mm:ss") %></span>
              </a>
            </li>
          </ItemTemplate>
        </asp:Repeater>
      </ul>
    </div>
  </form>
</body>
</html>