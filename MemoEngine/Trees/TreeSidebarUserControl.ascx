<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreeSidebarUserControl.ascx.cs" Inherits="MemoEngine.Trees.TreeSidebarUserControl" %>
<div class="container">
    <div class="row">
        <div class="col-md-12">
            <h3>사이드바 테스트</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <div class="panel-group" id="mmuSidebar" role="tablist"
                aria-multiselectable="true">
                <% foreach (var mi in Model)
                    {
                %>
                <div class="panel panel-default">
                    <div class="panel-heading" role="tab"
                        id="mmuSidebar-heading-<%= mi.TreeId %>">
                        <h4 class="panel-title">
                            <a role="button" data-toggle="collapse"
                                data-parent="#mmuSidebar"
                                href="#mmuSidebar-<%= mi.TreeId %>"
                                aria-expanded="true"
                                aria-controls="mmuSidebar-<%= mi.TreeId %>">
                                <%= mi.TreeName %>
                            </a>
                        </h4>
                    </div>
                    <div id="mmuSidebar-<%= mi.TreeId %>"
                        class="panel-collapse collapse in" role="tabpanel"
                        aria-labelledby="mmuSidebar-heading-one">
                        <div class="panel-body">
                            <ul>
                                <% foreach (var smi in mi.Trees)
                                    {
                                %>
                                <li>
                                    <a href="<%= smi.TreePath %>"
                                        target="<%= smi.Target %>">
                                        <%= smi.TreeName %>
                                    </a>
                                </li>
                                <%
                                    }
                                %>
                            </ul>
                        </div>
                    </div>
                </div>
                <%
                    }
                %>
            </div>
        </div>
    </div>
</div>
