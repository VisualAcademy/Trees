using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using Trees.Models;

namespace MemoEngine.Trees
{
    public partial class TreeSidebarUserControl : System.Web.UI.UserControl
    {
        // 모델 개체 생성: 모든 메뉴 가져오기 
        public List<Tree> Model { get; set; } = new List<Tree>();

        private int communityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            // 넘어온 CommunityId 바인딩
            if (Request.QueryString["CommunityId"] != null)
            {
                communityId =
                    Convert.ToInt32(Request.QueryString["CommunityId"]);
            }

            if (!Page.IsPostBack)
            {
                DisplayData();
            }
        }

        private void DisplayData()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var repository = new TreeRepository(connectionString);

            // 커뮤니티별 메뉴 전체 리스트(IsVisible 속성이 true인 것만 출력) 
            Model = repository.GetTrees();
        }
    }
}
