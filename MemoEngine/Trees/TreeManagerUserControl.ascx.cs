using Trees.Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace MemoEngine.Trees
{
    public partial class TreeManagerUserControl : System.Web.UI.UserControl
    {
        private readonly string _connectionString;
        private readonly TreeRepository _repository;

        public TreeManagerUserControl()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            _repository = new TreeRepository(_connectionString);
        }

        //private int communityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCommunity();
            }

            if (!Page.IsPostBack)
            {
                // 넘어온 CommunityId 바인딩
                if (Request.QueryString["CommunityId"] != null)
                {
                    // 드롭다운리스트 선택
                    ddlCommunity.SelectedValue = Request.QueryString["CommunityId"];
                }
            }

            if (!Page.IsPostBack)
            {
                DisplayData();
            }

            //// 커뮤니티가 선택된 상태에서만 메뉴 등록 버튼 활성화
            //if (ddlCommunity.SelectedValue == "0")
            //{
            //    btnAddTree.Enabled = false;
            //}
            //else
            //{
            //    btnAddTree.Enabled = true;
            //}
        }

        /// <summary>
        /// 커뮤니티 드롭다운리스트 바인딩
        /// </summary>
        private void BindCommunity()
        {
            // ddlCommunity.Items.Add(new ListItem("GOT7", "3"));
        }

        private void DisplayData()
        {
            // 커뮤니티별 메뉴 전체 리스트 
            ctlTreeLists.DataSource = _repository.GetAll(Convert.ToInt32(ddlCommunity.SelectedValue));
            ctlTreeLists.DataBind();
        }

        protected string FuncIsVisible(object objIsVisible)
        {
            string s = "";
            bool isVisible = Convert.ToBoolean(objIsVisible);

            if (isVisible)
            {
                s = "<input type='checkbox' name='chkIsVisible' value='true' "
                    + "checked onchange='valueChanged()' />";
            }
            else
            {
                s = "<input type='checkbox' name='chkIsVisible' value='false' "
                    + " onchange='valueChanged()' />";
            }

            return s;
        }

        protected string FuncIsBoard(object objIsBoard)
        {
            string s = "";
            bool isBoard = Convert.ToBoolean(objIsBoard);

            if (isBoard)
            {
                s = "<input type='checkbox' name='chkIsBoard' value='true' "
                    + "checked onchange='IsBoardValueChanged()' />";
            }
            else
            {
                s = "<input type='checkbox' name='chkIsBoard' value='false' "
                    + " onchange='IsBoardValueChanged()' />";
            }

            return s;
        }


        protected string FunStep(object objParentId)
        {
            string s = "";

            int parentId = Convert.ToInt32(objParentId);

            if (parentId == 0)
            {
                s = "<span class='btn btn-default'>상단메뉴</span>";
            }
            else
            {
                s = "<span class='badge text-right'>서브메뉴</span>";
            }

            return s;
        }

        protected void btnUpdateTree_Click(object sender, EventArgs e)
        {
            List<Tree> lst = new List<Tree>();

            string[] txtTreeIds = Request.Form.GetValues("txtTreeId");
            string[] txtTreeNames = Request.Form.GetValues("txtTreeName");
            string[] txtTreePaths = Request.Form.GetValues("txtTreePath");
            string[] hdnIsVisibles = Request.Form.GetValues("hdnIsVisible");
            string[] txtTreeOrders = Request.Form.GetValues("txtTreeOrder");

            string[] hdnIsBoards = Request.Form.GetValues("hdnIsBoard");
            string[] txtBoardAlias = Request.Form.GetValues("txtBoardAlias");

            string[] lstTarget = Request.Form.GetValues("lstTarget");


            for (int i = 0; i < txtTreeIds.Length; i++)
            {
                Tree mm = new Tree();
                mm.TreeId = Convert.ToInt32(txtTreeIds[i]);
                mm.TreeName = txtTreeNames[i];
                mm.TreePath = txtTreePaths[i];

                // 표시 여부
                if (hdnIsVisibles != null)
                {
                    if (hdnIsVisibles[i] != null)
                    {
                        mm.IsVisible = Convert.ToBoolean(hdnIsVisibles[i]);
                    }
                }
                // 게시판 여부
                if (hdnIsBoards != null)
                {
                    if (hdnIsBoards[i] != null)
                    {
                        mm.IsBoard = Convert.ToBoolean(hdnIsBoards[i]);
                    }
                }
                // 게시판 별칭
                if (txtBoardAlias != null)
                {
                    if (txtBoardAlias[i] != null)
                    {
                        mm.BoardAlias = txtBoardAlias[i];
                    }
                }
                // 타겟
                if (lstTarget != null)
                {
                    if (lstTarget[i] != null)
                    {
                        mm.Target = lstTarget[i];
                    }
                }

                mm.TreeOrder = Convert.ToInt32(txtTreeOrders[i]);

                lst.Add(mm);
            }

            _repository.Update(lst);

            // 다시 데이터 표시
            // DisplayData();
            // 현재 페이지 다시 로드
            Response.Redirect(Request.RawUrl);
        }

        /// <summary>
        /// 체크박스로 선택된 메뉴 항목 삭제
        /// </summary>
        protected void btnDeleteTreeItem_Click(object sender, EventArgs e)
        {
            // 체크박스로 선택된 TreeId를 배열로 받기
            string[] arrTreeIds = Request.Form.GetValues("chkSelect");

            // 널값 체크
            if (arrTreeIds != null)
            {
                for (int i = 0; i < arrTreeIds.Length; i++)
                {
                    // 삭제 처리
                    _repository.Remove(Convert.ToInt32(arrTreeIds[i]));
                }

                // 현재 페이지 다시 로드
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void ddlTopOrSub_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (ddlTopOrSub.SelectedValue == "0")
            {
                // 상단메뉴만 등록
                ddlParentTree.Items.Clear();
                ddlParentTree.Items.Add(new ListItem("상단메뉴", "0"));
                ddlParentTree.Enabled = false;
            }
            else
            {
                ddlParentTree.Enabled = true;

                // DB에서 ParentId가 0인 자료 바인딩
                ddlParentTree.DataTextField = "TreeName";
                ddlParentTree.DataValueField = "TreeId";
                ddlParentTree.DataSource = _repository.GetTreesByParentId(0, Convert.ToInt32(ddlCommunity.SelectedValue));
                ddlParentTree.DataBind();

                if (ddlParentTree.Items.Count == 0)
                {
                    // 상단메뉴만 등록
                    ddlParentTree.Items.Add(new ListItem("상단메뉴", "0"));
                    ddlParentTree.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 게시판 여부 체크박스 체크
        /// </summary>
        protected void chkIsBoard_CheckedChanged(
            object sender, EventArgs e)
        {
            if (chkIsBoard.Checked)
            {
                ddlBoardList.Enabled = true;

                BindBoardList();
            }
            else
            {
                ddlBoardList.Enabled = false;
                txtTreeName.Text = txtTreePath.Text = "";
            }
        }

        private void BindBoardList()
        {
            // 수작업으로 게시판 정보 바인딩
            ddlBoardList.Items.Add(new ListItem("공지사항", "Notice"));
            ddlBoardList.Items.Add(new ListItem("자유게시판", "Free"));
            ddlBoardList.Items.Add(new ListItem("앨범", "Photo"));
            // 실제로는 DB에서 게시판 정보를 읽어서 바인딩
        }

        /// <summary>
        /// 게시판 리스트 드롭다운리스트 선택시 텍스트박스 2개 채우기
        /// </summary>
        protected void ddlBoardList_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            txtTreeName.Text = ddlBoardList.SelectedItem.Text;
            txtTreePath.Text =
                $"/BoardList?BoardName={ddlBoardList.SelectedValue}";

            // 게시판 별칭을 추가
            hdnBoardAlias.Value = ddlBoardList.SelectedValue;
        }

        protected void btnAddTree_Click(object sender, EventArgs e)
        {
            Tree mnu = new Tree();
            mnu.CommunityId = Convert.ToInt32(ddlCommunity.SelectedValue);
            mnu.ParentId = Convert.ToInt32(ddlParentTree.SelectedValue);
            mnu.IsBoard = chkIsBoard.Checked;
            mnu.TreeName = txtTreeName.Text;
            mnu.TreePath = txtTreePath.Text;
            mnu.Target = ddlTarget.SelectedValue;
            mnu.IsVisible = optIsVisible.SelectedValue == "1" ? true : false;

            // ParentId가 0이면 가장 나중에 서브 메뉴이면 해당 메뉴 +1 순서에
            mnu.TreeOrder = _repository.UpdateTreeOrder(mnu.ParentId, mnu.CommunityId);

            mnu.BoardAlias = hdnBoardAlias.Value;

            _repository.Add(mnu);

            Response.Redirect(Request.RawUrl);
        }

        protected void ddlCommunity_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            string strCommunityId = ddlCommunity.SelectedValue;

            if (strCommunityId != "0")
            {
                string url =
                Request.ServerVariables["SCRIPT_NAME"] +
                    "?CommunityId=" + strCommunityId;
                Response.Redirect(url);
            }
            else
            {
                Response.Redirect(Request.ServerVariables["SCRIPT_NAME"]);
            }
        }

        protected string FuncTargetCombo(string target)
        {
            string s =
                "<select name='lstTarget' class='form-control'>";

            if (target == "_self")
            {
                s += "<option value='_self' selected>현재창</option>";
                s += "<option value='_blank'>새 창</option>";
            }
            else
            {
                s += "<option value='_self'>현재창</option>";
                s += "<option value='_blank' selected>새 창</option>";
            }

            s += "</select>";

            return s;
        }
    }
}
