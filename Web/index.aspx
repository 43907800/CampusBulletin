<%@ Page Title="" Language="C#" MasterPageFile="~/IndexPage/IndexParent.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Web.index" %>
<%@ Import Namespace="Model" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {

            loadBulleits("/Ashx/BulletinManage/BulletinPageList.ashx");//加载首页列表
            BindNavAClick();//导航 A 标签绑定 点击事件
            BindLoginShowBtnClick();//绑定登陆 弹出 按钮的点击事件
            BindPostShowBtnClick();//绑定发帖框 弹出按钮的点击事件
            BindInfoEditShowBtnClick();//绑定个人信息框 弹出按钮的点击事件
            BindLoginBtnClick();//登陆按钮绑定点击事件
            BindRegisterShowBtnClick();//绑定注册框 弹出按钮的点击事件
            loadEditor();//加载富文本编辑器
            BindBulletinAddBtnClick();//发帖按钮绑定单击事件
            BindUserAddBtnClick();//给注册按钮绑定click 事件
            BindCommentBtnClick(); //评论按钮添加绑定事件
        });
        //加载首页列表
        function loadBulleits(url) {
            //Ashx/BulletinManage/BulletinPageList.ashx
            $.ajax({
                url: url,
                type: "get",
                dataType: "json",
                success: function (data) {
                    vm.list = data.List;
                    vm.NavHtml = data.NavHtml;
                    //alert(data);
                    setTimeout(function () {
                        BindPageNavClick();//绑定 分页 a 标签 单击事件
                    }, 100)
                }
            });
        }
        //绑定 分页 a 标签 单击事件
        function BindPageNavClick() {
            $("#NavDiv a").click(function () {
                var href = $(this).attr("href");
                var quertStr = href.substr(href.lastIndexOf('?') + 1);
                loadBulleits("/Ashx/BulletinManage/BulletinPageList.ashx?" + quertStr);
                return false;
            });
        }
        //导航 A 标签绑定 点击事件
        function BindNavAClick() {
            $(".typeNav").click(function () {
                var hrefUrl = $(this).attr("href");
                //alert(hrefUrl);
                loadBulleits(hrefUrl);
                ListShowDetailsHide();
                return false;
            });
        }
        //列表显示 详情隐藏
        function ListShowDetailsHide() {
            $("#list").css("display", "block");
            $("#Details").css("display", "none");
        }
        //列表隐藏 详情显示
        function DetailsShowListHide() {
            $("#Details").css("display", "block");
            $("#list").css("display", "none");
        }
        //绑定登陆框 弹出按钮的点击事件
        function BindLoginShowBtnClick() {
            $("#ShowLoginModel").click(function () {
                $('#LoginModal').modal('show');
            });
        }

        //登陆按钮绑定点击事件
        function BindLoginBtnClick() {
            $("#BtnLogin").click(function () {
                // alert(111);
                var frmdata = $("#frmLogin").serialize();
                $.ajax({
                    url: "/Ashx/UserManage/UserLogin.ashx",
                    data: frmdata,
                    success: function (resdata) {
                        var data = resdata.split(":");
                        if (data[0] == "Ok" && data[1] == "成功") {
                            location.href = "/index.aspx";
                            // alert(data[1]);
                        } else if (data[0] == "No") {
                            alert(data[1]);
                        } else {
                            alert("未知错误");
                        }
                    }
                });
                return false;
            });
        }

        //绑定注册框 弹出按钮的点击事件
        function BindRegisterShowBtnClick() {
            $("#ShowRegisterModal").click(function () {
                $('#RegisterModal').modal('show');
                BindSelectChange();//为省下拉列表绑定 change  事件 并加载 省下拉列表
            });
        }

        //绑定个人信息框 弹出按钮的点击事件
        function BindInfoEditShowBtnClick() {
            $("#ShowInfoEditModal").click(function () {
                var url = "/Ashx/UserManage/UserEdit.ashx?Id=<%=userId%>";
                //alert(url);
                $.ajax({
                    url: url,
                    type: "get",
                    async: false,
                    dataType: "json",
                    success: function (data) {
                        $("#EditId").val(data.Id);
                        $("#EditUserName").val(data.UserName);
                        $("#ShowEditUserName").val(data.UserName);
                        $("#EditPassword").val(data.Password);
                        $("#EditPassword1").val(data.Password);
                        $("#EditEmail").val(data.Mail);
                        $("#EditName").val(data.Name);
                        $("#EditSex").val(data.Sex == false ? "女" : "男");
                        var region = data.Region;
                        $("#EditRegion").val(region);
                        //alert(region.split(" "));
                        $("#EditselPro").append("<option selected> " + region.split(" ")[1] + "</option>");
                        $("#EditselCity").append("<option selected> " + region.split(" ")[2] + "</option>");
                      
                        //alert(data.Text);
                        BondOneSelClick(); //为下拉框绑定一次点击事件 加载 数据
                    }
                });


                $('#InfoEditModal').modal('show');//弹出模态框
                BindEditSelectChange();//为 编辑个人信息 省下拉列表绑定 change  事件 
                BindBtnEditClick();//为更新按钮绑定点击事件
            });
        }

        //绑定发帖框 弹出按钮的点击事件
        function BindPostShowBtnClick() {
            $("#ShowPostModel").click(function () {
                $('#PostModal').modal('show');
                loadTypeSel("#selPro");//加载公告类型下拉框

                return false;
            });
        }

        var editor;
        //加载富文本编辑器
        function loadEditor() {
            var E = window.wangEditor
            editor = new E('#toolbar', '#Editor');
            //设置图片上传至 服务器
            editor.customConfig.uploadImgServer = '/Ashx/BulletinManage/UpLodeImg.ashx'
            //editor.customConfig.uploadImgShowBase64 = true
            editor.create();
            $('#div1').attr('style', 'height:auto;');
        }
        //加载公告类型下拉框
        function loadTypeSel(selProidStr) {
            $.ajaxSettings.async = false;
            $.getJSON("/Ashx/BulletinTypeManage/TypePageList.ashx", "", function (data) {
                $(selProidStr).html("");
                for (var i in data.List) {
                    $(selProidStr).append("<option value='" + data.List[i].Id + "'>" + data.List[i].TypeName + "</option>");
                }

            })
            $.ajaxSettings.async = true;
        }

        //发帖按钮绑定单击事件
        function BindBulletinAddBtnClick() {
            $("#Add").click(function () {
                var title = $("#Title").val();
                var content = editor.txt.html();
                title = title.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                content = content.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                var frmData = { "TypeId": $("#selPro").val(), "UserName": $("#UserName").val(), "content": content, "Title": title };
                $.ajax({
                    url: "/Ashx/BulletinManage/BulletinAdd.ashx",
                    type: "post",
                    data: frmData,
                    success: function (serverdata) {
                        var data = serverdata.split(":");
                        if (data[0] == "Ok" && data[1] == "成功") {
                            alert("添加成功!");
                            location.href = "/index.aspx";//重定向首页

                        } else if (data[0] == "Ok") {
                            alert(data[1]);
                            location.href = "/index.aspx";//重定向首页

                        } else if (data[0] == "No") {
                            alert(data[1]);
                        } else {
                            alert("添加失败!");
                        }
                    }
                });
                //$('#PostModal').modal('hide');
                return false;
            });
        }


        //加载 地区下拉列表数据
        function LoadSel(select, pid) {
            $.getJSON("/Ashx/RegionManage/FindRegion.ashx", { "Pid": pid }, function (data) {
                select.html("");
                for (var i in data) {
                    select.append("<option title='" + data[i].Name + "' value='" + data[i].Id + "'> " + data[i].Name + "</option>");
                }
                //$("#selPro").change();
                $("#selCity").change();
                $("#EditselCity").change();
            });
        }

        //为省下拉列表绑定 change  事件 并加载 省下拉列表
        function BindSelectChange() {
            $("#selProvince").change(function () {
                LoadSel($("#selCity"), $("#selProvince").val());
            });
            $("#selCity").change(function () {
                var region = $("#selProvince").find("option:selected").text() + $("#selCity").find("option:selected").text();
                $("#Region").val(region);
            })

            LoadSel($("#selProvince"), 0);
            LoadSel($("#selCity"), 1);
        }

        //给注册数据按钮绑定click 事件
        function BindUserAddBtnClick() {
            $("#UserAdd").click(function () {

                var frmData = $("#frmRegister").serialize();
                $.ajax({
                    url: "/Ashx/UserManage/UserAdd.ashx",
                    type: "post",
                    data: frmData,
                    success: function (data) {
                        if (data == "Ok") {
                            alert("注册成功!");
                            //InitTable();
                            location.href = "/index.aspx";
                        } else if (data == "No") {
                            alert("注册失败!");
                        } else { alert(data); }

                    }
                });
                return false;
            })
        }


        //为下拉框绑定一次点击事件 加载 数据
        function BondOneSelClick() {
            $(".EditSel").bind("click", function () {
                LoadSel($("#EditselPro"), 0);
                LoadSel($("#EditselCity"), 1);
                $(".EditSel").unbind('click');
            })
        }
        //为 编辑信息 省下拉列表绑定 change  事件 
        function BindEditSelectChange() {
            $("#EditselPro").change(function () {
                LoadSel($("#EditselCity"), $("#EditselPro").val());

            });
            $("#EditselCity").change(function () {
                var region = $("#EditselPro").find("option:selected").text() + $("#EditselCity").find("option:selected").text();
                $("#EditRegion").val(region);
            })
        }

        //为信息更新按钮绑定点击事件
        function BindBtnEditClick() {
            $("#BtnEdit").click(function () {
                var editFrm = $("#frmInfoEdit").serialize();
                $.ajax({
                    url: "/Ashx/UserManage/UserEdit.ashx",
                    type: "post",
                    data: editFrm,
                    success: function (data) {
                        if (data == "Ok") {
                            alert('更新成功,重新登录后生效!!!');
                            //InitTable();
                            $('#EditModal').modal('hide');
                            location.href = "/index.aspx";
                        } else if (data == "No") {
                            alert("更新失败!!!");
                        } else {
                            alert(data);
                        }

                    }
                });

                return false;
            });
        }

        //评论按钮添加绑定事件
        function BindCommentBtnClick() {
            $("#BtnComment").click(function () {
                var bulletitsId = $("#bulleitsId").val();
                var userName = $("#userName").val();
                var content = $("#txtarea").val();
                content = content.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                $.ajax({
                    url: "/Ashx/DiscussManege/DiscussAdd.ashx",
                    type: "post",
                    data: { "bulletitsId": bulletitsId, "userName": userName, "content": content },
                    success: function (data) {
                        if (data=="Ok") {
                            alert("评论成功!!!");
                            LodeDiscussList(bulletitsId);//加载评论 信息
                        }else if(data=="No")
                            alert("评论失败!!!");
                        else
                            alert(data);
                    }
                });
            });
        }

        //加载评论 信息
        function LodeDiscussList(id) {
            //var bulletitsId = $("#bulleitsId").val();
            $.ajax({
                url: "/Ashx/DiscussManege/DiscussList.ashx",
                type: "post",
                dataType:"json",
                data: { "bulletitsId": id },
                success: function (data) {
                   // alert(data);
                    vm.Discuss = data;
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="app">
        
         <!---------------------------列表i展示-------------->
        <div id="list" >
        <div class="post-list-item" v-for="item in list">
            <div class="col-xs-3">
                <a @click.stop="TitleAClick(item.Id)" href="#" class="BulletinDetails" id="aaa">
                    <img class="post-object listImg"  v-bind:src="getImgSrc(item.Content)" />
                    
                </a>
            </div>
            <div class="col-xs-9">
                <a @click.stop="TitleAClick(item.Id)"  href="#" class="BulletinDetails">
                    <div class="post-heading">{{item.Title}}</div>
                </a>
                <p v-html="listShowContent(item.Content)">
                   <%--{{listShowContent(item.Content)}}--%>
                </p>
                <p class="text-muted">
                    <span>
                        <img class="avatar" v-bind:src="item.User.HeadPicAddress">
                        {{item.UserName}}
                    </span>⋅
                           <span>点击:{{item.Clicks}}
                            </span>⋅
                    <span>{{timeConvert(item.ReleaseTime)}}</span>
                </p>
            </div>
        </div>
        <div v-html="NavHtml" id="NavDiv"></div>
         <!---------------------------列表i展示结束-------------->

    </div>
         <!---------------------------详情展示-------------->

        <div id="Details" style="display:none">
            <div class="col-md-12 news">
                <h1 class="title text-center">{{Bulleits.Title}}</h1>
                <p class="info">
                  <span>点击：{{Bulleits.Clicks}}</span> •
                   <span>{{Bulleits.UserName}}</span> •
                   <span>{{timeConvert(Bulleits.ReleaseTime)}}</span>
                    <input type="hidden" id="bulleitsId" v-bind:value="Bulleits.Id" />
                </p>
                <div class="content" v-html="Bulleits.Content">
                  
                </div>
            </div>
            
            <div class="col-md-12 comment  container">
                <div class="row container col-md-12">
                    <hr style="margin-left:20px"/>
                    <textarea class="txtarea" id="txtarea"> </textarea>
                   <br /> 
                    <button class="btn btn-default col-md-offset-10"  id="BtnComment" style="width:100px"  >评论</button>
                </div>
                <div class="row container col-md-12">
                    <div class="row container col-md-12" v-for="item in Discuss">
                         <hr style="margin-left:20px"/>
                    <span style="margin-left:20px">{{item.UserName}}-[{{timeConvert(item.DiscussTime)}}]: {{item.Content}}</span>
                    </div>
                    
                    
                    
                </div>
            </div>

        </div>
            <!---------------------------详情展示结束-------------->
            <!------------------------登陆模态框-------------->
            <div class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" data-backdrop="false"  id="LoginModal" style="margin-top:200px">
              <div class="modal-dialog  modal-sm" role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">用户登陆</h4>
                  </div>
                  <div class="modal-body">
                    <form class="form-horizontal" id="frmLogin">

                        <div class="form-group">
                            <label class="col-sm-3 control-label">用户名</label>
                            <div class="col-sm-8">
                                <input type="text" class="form-control" name="LoginUserName" id="LoginUserName" required />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-3 control-label">密码</label>
                            <div class="col-sm-8">
                                <input type="password" class="form-control" name="LoginPassword" id="LoginPassword" required/>
                            </div>
                        </div>
                     

                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-6">
                                <input type="submit" style="width:100px" class="btn btn-info" value="登陆" id="BtnLogin"  />

                            </div>

                        </div>
                    </form>
                  </div>

                  <%--<div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                  </div>--%>
                </div><!-- /.modal-content -->
              </div><!-- /.modal-dialog -->
            </div><!-- /.modal -->
            <!---------------------------登陆模态框结束-------------->
            <!---------------------------注册模态框-------------->
        <div class="modal fade" tabindex="-1" role="dialog" data-backdrop="false"  id="RegisterModal" >
              <div class="modal-dialog " role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">用户注册</h4>
                  </div>
                  <div class="modal-body">
                    <form class="form-horizontal" id="frmRegister">

                        <div class="form-group">
                <label class="col-sm-4 control-label">用户名</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control" name="UserName" id="RegUserName" required/>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-4 control-label">密码</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control" name="Password" id="Password" required/>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-4 control-label">确认密码</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control" name="Password1" id="Password1" required/>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-4 control-label">邮箱</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control" name="Email" id="Email" required/>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-4 control-label">昵称</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control" name="Name" id="Name"/>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-4 control-label">性别</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control" name="Sex" id="Sex"/>
                </div>
            </div>
            <div class="form-group">
                <input type="hidden" name="Region"  id="Region"/>
                <label class="col-sm-4 control-label">地区</label>
                <div class="col-sm-6">
                    <div class="col-sm-6">
                        <select class="selectpicker1 form-control"  id="selProvince">
                          
                            <option title=""></option>
                        </select>
                    </div>
                    <div class="col-sm-6">
                        <select class="selectpicker1 form-control " data-live-search="true" id="selCity">
                         

                        </select>
                    </div>
                </div>
            </div>


            <div class="form-group">
                <div class="col-sm-offset-4 col-sm-10">
                    <input type="submit" style="width:100px" class="btn btn-danger" value="注册" id="UserAdd" />
                </div>
            </div>

                    </form>
                  </div>

                  <%--<div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                  </div>--%>
                </div><!-- /.modal-content -->
              </div><!-- /.modal-dialog -->
            </div><!-- /.modal -->



            <!---------------------------登陆模态框结束-------------->
         <!------------------------发帖模态框-------------->
            <div class="modal fade  bs-example-modal-lg" tabindex="-1" role="dialog" data-backdrop="false"  id="PostModal" >
              <div class="modal-dialog  modal-lg" role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">发帖</h4>
                  </div>
                  <div class="modal-body">
                    <form class="form-horizontal" id="frmPost">

                        <div class="form-group">
                <label class="col-sm-1 control-label">类别</label>
                <div class="col-sm-3">
                    <select class="selectpicker1 form-control" id="selPro">

                        <option title=""></option>
                    </select>
                </div>
                <label class="col-sm-2 control-label">发布人</label>
                <div class="col-sm-3">
                    <input type="text" class="form-control" name="UserName" id="UserName" required value='<%=userName %>'/ readonly="readonly">
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-1 control-label">标题</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" name="Title" id="Title" required />
                </div>
            </div>
            <div class="form-group">
                <!--富文本 工具条-->
                <div id="toolbar" style="border: 1px solid #ccc;"></div>
                <!--富文本 文本框-->
                <div id="Editor" style="height:550px;border: 1px solid #ccc" >
                </div>
             </div>

                <div class="form-group">
                    <div class="col-sm-10">
                        <input type="submit" style="width:100px" class="btn btn-default" value="发布" id="Add" />
                    </div>
                </div>

                        
                    </form>
                  </div>

                  <%--<div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                  </div>--%>
                </div><!-- /.modal-content -->
              </div><!-- /.modal-dialog -->
            </div><!-- /.modal -->
            <!---------------------------发帖模态框结束-------------->

                 <!------------------------个人信息框-------------->
            <div class="modal fade " tabindex="-1" role="dialog" data-backdrop="false"  id="InfoEditModal" >
              <div class="modal-dialog " role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">个人信息</h4>
                  </div>
                  <div class="modal-body">
                    <form class="form-horizontal" id="frmInfoEdit">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">用户名</label>
                            <div class="col-sm-6">
                                <input type="hidden" name="EditId" id="EditId" />
                                
                                <input type="text" class="form-control" name="EditUserName" id="EditUserName" required readonly="readonly">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">密码</label>
                            <div class="col-sm-6">
                                <input type="password" class="form-control" name="EditPassword" id="EditPassword" required>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">确认密码</label>
                            <div class="col-sm-6">
                                <input type="password" class="form-control" name="EditPassword1" id="EditPassword1" required>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">邮箱</label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control" name="EditEmail" id="EditEmail" required>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">昵称</label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control" name="EditName" id="EditName">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">性别</label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control" name="EditSex" id="EditSex">
                            </div>
                        </div>
                        <div class="form-group">
                            <input type="hidden" name="EditRegion" id="EditRegion" />
                            <label class="col-sm-4 control-label">地区</label>
                            <div class="col-sm-6">
                                <div class="col-sm-6">
                                    <select class="selectpicker1 form-control EditSel" id="EditselPro"></select>
                                </div>
                                <div class="col-sm-6">
                                    <select class="selectpicker1 form-control EditSel" data-live-search="true" id="EditselCity"></select>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-4 col-sm-6">
                                <input type="submit" style="width:150px" class="btn btn-primary" value="更新" id="BtnEdit" />

                            </div>

                        </div>

                    </form>
                  </div>

                  <%--<div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                  </div>--%>
                </div><!-- /.modal-content -->
              </div><!-- /.modal-dialog -->
            </div><!-- /.modal -->
            <!---------------------------个人信息框结束-------------->

 </div>
    <script type="text/javascript">
        var vm = new Vue({
            el: "#app",
            data: {
                list: "",
                NavHtml: "",
                Bulleits: "",
                Discuss:[]
            },
            methods: {
                TitleAClick(id) {//文章的标题 和图片的点击事件
                    $.get("/Ashx/BulletinManage/BulletinEdit.ashx?Id=" + id, {}, function (data) {
                        data.Content = data.Content.replace(/&lt;/g, '<').replace(/&gt;/g, '>');
                        vm.Bulleits = data;
                        DetailsShowListHide();//列表隐藏 详情显示
                        LodeDiscussList(id);//加载评论 信息
                    }, "json")
                    //alert(id);
                    return false;
                }, getImgSrc(str) {
                    str = str.replace(/&lt;/g, '<').replace(/&gt;/g, '>');
                    var imgAddra = [];
                    var imgReg = /<img.*?(?:>|\/>)/gi;
                    //匹配src属性
                    var srcReg = /src=[\'\"]?([^\'\"]*)[\'\"]?/i;
                    var arr = str.match(imgReg);
                    if (arr == null) return "/img/default.png";
                    //alert('所有已成功匹配图片的数组：' + arr);
                    for (var i = 0; i < arr.length; i++) {
                        var src = arr[i].match(srcReg);
                        //获取图片地址
                        if (src[1]) {
                            imgAddra[i] = src[1];
                        }
                    }
                    return imgAddra[0];
                }, timeConvert(str) {
                    if (str == null) return "";
                    var date = eval('new ' + str.substr(1, str.length - 2)); //new Date()

                    var year = date.getFullYear(),
                    month = date.getMonth() + 1,//月份是从0开始的
                    day = date.getDate(),
                    hour = date.getHours(),
                    min = date.getMinutes(),
                    sec = date.getSeconds();
                    var newTime = year + '-' +
                                month + '-' +
                                day + ' ' +
                                hour + ':' +
                                min + ':' +
                                sec;
                    return newTime;
                }, listShowContent(str) {
                    str = str.replace(/&lt;/g, '<').replace(/&gt;/g, '>');
                    if (str.length > 20) {
                        str = str.substr(0, 50);
                        str += "...";
                    }
                    return str;
                }

            }, mounted() {
            }

        });


    </script>
</asp:Content>
