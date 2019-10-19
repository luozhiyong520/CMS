<%@ Control Language="C#" Inherits="UserControlView<AdminControlModel>" %>
 <script language="javascript">
     $(function () {
         if ($("#menu0")) {
             $("#main").attr("src", $("#menu0 li:first-child a").attr("href"));
         } 
     })
 </script>

  <%
      int j=0;
      foreach (AdminMenuModule adminOneMenu in Model.AdminOneMenuModuleList) {
          if (j == 0)
          {
          %>
      
    <ul id="menu<%=j%>" style="display:block">
    <%}
          else
          { %>
            <ul id="menu<%=j%>" style="display:none">
    <%} %>
    <%
          int k=0;
      foreach (AdminMenuModule adminTwoMenu in Model.TwoMenu[adminOneMenu.ModuleId])
      {
          string TargetUrl = adminTwoMenu.TargetUrl;

          if (TargetUrl.IndexOf('?') >= 0)
          {
              TargetUrl = TargetUrl + "&ModuleId=" + adminTwoMenu.ModuleId;
          }
          else
          {
              TargetUrl = TargetUrl + "?ModuleId=" + adminTwoMenu.ModuleId;
          }
          if (k == 0)
          { 
              %>
        <li><a target="main" class="links" onclick="ShowLinks(this)" href="<%=TargetUrl%>"><%=adminTwoMenu.ModuleName%></a></li>
        <%}
          else
          { %>
        <li><a target="main" onclick="ShowLinks(this)" href="<%=TargetUrl%>"><%=adminTwoMenu.ModuleName%></a></li>
        <%} %>
       <%k++;
      } %>
    </ul>
     
     <%j++;
      } %>
 
