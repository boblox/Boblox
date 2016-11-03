using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Models;
using umbraco;
using umbraco.cms.businesslogic.language;
using umbraco.cms.businesslogic.relation;
using umbraco.cms.businesslogic.web;
using umbraco.NodeFactory;

namespace Web.Helpers
{
    [Obsolete]
    public static class LanguageHelper
    {
        public static List<LanguageData> GetUrlsAndLanguages(int nodeid)
        {
            var dataList = new List<LanguageData>();

            var masterchildren = new Relation[] { };
            List<Domain> domains = GetDomains();
            int masternodeid = GetMasterLanguageNode(nodeid, ref masterchildren);
            int pageid;
            int nodelang = GetLanguageIdFromNodeId(nodeid);
            var url_lang = new Dictionary<string, string>();
            foreach (var lang in Language.GetAllAsList())
            {
                LanguageData data = new LanguageData();
                data.NativeName = System.Globalization.CultureInfo.GetCultureInfo(lang.CultureAlias.Substring(0, 2)).NativeName;
                data.LanguageCode = lang.CultureAlias.Substring(0, 2);
                data.CountryCode = lang.CultureAlias.Substring(lang.CultureAlias.Length - 2).ToLower();
                if (nodelang == lang.id)
                {
                    data.Url = "";
                }
                else
                {
                    if (GetChildren(masternodeid, masterchildren).TryGetValue(lang.id, out pageid))
                    {
                        data.Url = library.NiceUrl(pageid);
                    }
                    else if ((domains.Where(d => d.Language.id == lang.id)).Count() > 0)
                    {
                        data.Url = "http://" + (domains.Where(d => d.Language.id == lang.id)).First().Name;
                    }
                }
                if (data.Url != null)
                    dataList.Add(data);
            }
            return dataList;
        }

        private static int GetLanguageIdFromNodeId(int nodeId)
        {
            var domains = library.GetCurrentDomains(nodeId);

            if (domains != null && domains.Length > 0)
            {
                return domains[0].Language.id;
            }
            return 0;
        }

        private static int GetMasterLanguageNode(int nodeid, ref Relation[] masterChildren)
        {
            int pageid = 0;

            List<int> children = new List<int>();

            foreach (Relation relation in Relation.GetRelations(nodeid))
            {
                if (nodeid == relation.Parent.Id) // we're already at the "master" language so find childs
                {
                    masterChildren = Relation.GetRelations(nodeid);
                    return nodeid;
                }
                else if (nodeid == relation.Child.Id) // we're at a child so need to go up first to find the "master"
                {
                    masterChildren = Relation.GetRelations(relation.Parent.Id);
                    return relation.Parent.Id;
                }
            }
            return pageid;
        }

        public static Dictionary<int, int> GetChildren(int id, Relation[] relations)
        {
            var children = new List<int>();

            // also add the master
            children.Add(id);

            foreach (Relation relation in relations)
            {
                if (id == relation.Parent.Id)
                {
                    children.Add(relation.Child.Id);
                }
            }

            // match nodes with languages and return result
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            foreach (int child in children)
            {
                int langid = GetLanguageIdFromNodeId(child);
                if (langid != 0)
                    dictionary.Add(langid, child);
            }
            return dictionary;
        }

        public static List<Domain> GetDomains()
        {
            var alldomains = new List<Domain>();
            Node parent = new Node(-1);
            foreach (Node child in parent.Children)
            {
                Domain[] domains = library.GetCurrentDomains(child.Id);
                if (domains != null && domains.Length >= 0)
                {
                    foreach (Domain domain in domains)
                    {
                        alldomains.Add(domain);
                    }
                }
            }
            return alldomains;
        }
    }

}

