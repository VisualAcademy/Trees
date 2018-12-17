<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TreeManager.aspx.cs" Inherits="MemoEngine.Trees.TreeManager" %>

<%@ Register Src="~/Trees/TreeManagerUserControl.ascx" TagPrefix="uc1" TagName="TreeManagerUserControl" %>
<%@ Register Src="~/Trees/TreeSidebarUserControl.ascx" TagPrefix="uc1" TagName="TreeSidebarUserControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>트리 메뉴 관리자</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.3.1.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:TreeManagerUserControl runat="server" id="TreeManagerUserControl" />
            <hr />
            <uc1:TreeSidebarUserControl runat="server" id="TreeSidebarUserControl" />
        </div>
    </form>
</body>
</html>
