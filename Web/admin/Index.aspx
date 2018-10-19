<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Web.admin.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="/lib/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/js/jquery-1.11.1.js"></script>
    <script src="/lib/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>

</head>
<body>
    <div class="container-fluid">
        <!---------------------------顶部栏----------------------------->
        <nav class="navbar navbar-default navbar-static-top">
            <div class="container">
                <div class="navbar-header">
                    <a class="navbar-brand" href="#">后台管理系统</a>
                </div>
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <p class="navbar-text navbar-right">欢迎, <%=adminName %> 管理员&nbsp;<a href="#">退出</a></p>
                </div>
            </div>
        </nav>
        <!----------------------顶部栏结束-------------------------------->


        <div class="row">
            <!---------------------------导航栏----------------------------->
            <div class="col-sm-2">
                <ul class="nav nav-pills nav-stacked">
                    <li role="presentation" class="active"><a href="#">首页设置</a></li>
                    <li role="presentation"><a href="#">用户管理</a></li>
                    <li role="presentation" class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">用户管理 <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="/admin/UserList.html" target="content">用户列表</a></li>
                            <li><a href="#">添加用户</a></li>
                        </ul>
                    </li>
                    <li role="presentation" class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">公告管理 <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="#">公告列表</a></li>
                            <li><a href="/admin/BulletinType.html" target="content">公告类别</a></li>
                        </ul>
                    </li>
                    <li role="presentation" class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">系统设置 <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="/admin/NavList.html" target="content">导航栏</a></li>
                            <li><a href="/admin/SensitiveList.html" target="content">敏感词</a></li>
                            <li><a href="#">配置信息</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>

            <!----------------------导航栏结束-------------------------------->
            <div class="col-sm-10">
               <iframe name="content" frameborder="0" scrolling="no" height="800px" width="100%" ></iframe>
        

            </div>
    </div>

</body>
</html>
