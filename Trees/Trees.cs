using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Trees.Models;

namespace Trees
{
    class Trees
    {
        static void Main()
        {
            // [1] 인-메모리 컬렉션 사용 
            var inMemoryTree = new TreeProviderContainer(new TreeDataInMemory());
            TreePrint(inMemoryTree.GetTrees());

            // [2] XML 파일 사용
            var xmlUrl = Path.Combine(
                Directory.GetCurrentDirectory(), "App_Data\\Trees.xml");
            var xmlTree = new TreeProviderContainer(
                new TreeDataInXml(xmlUrl));
            TreePrintAll(xmlTree.GetTrees());

            // [3] SQL 데이터베이스 사용 
            var connectionString = ConfigurationManager.ConnectionStrings[
                "ConnectionString"].ConnectionString;
            var sqlTree = new TreeProviderContainer(
                new TreeDataInSql(connectionString));
            TreePrintAll(sqlTree.GetTrees());
        }

        // 대분류/중분류 출력 
        private static void TreePrint(List<Tree> trees)
        {
            foreach (var tree in trees)
            {
                Console.WriteLine($"{tree.TreeId} - {tree.TreeName}");
                if (tree.Trees.Count > 0)
                {
                    foreach (var subTree in tree.Trees)
                    {
                        Console.WriteLine($"\t{subTree.TreeId} - {subTree.TreeName}");
                    }
                }
            }
        }

        // 무한 트리 출력 
        private static int tabCount = 0; 
        private static void TreePrintAll(List<Tree> trees)
        {
            foreach (var tree in trees)
            {
                // 탭 카운트만큼 들여쓰기
                var tab = "";
                foreach (var t in Enumerable.Repeat("\t", tabCount))
                {
                    tab += t; 
                }
                Console.WriteLine($"{tab}{tree.TreeId} - {tree.TreeName}");
                if (tree.Trees.Count > 0)
                {
                    tabCount++; // 탭 카운트 증가
                    TreePrintAll(tree.Trees); // 재귀 호출
                    tabCount--; // 탭 카운트 감소
                }
            }
        }
    }
}
