//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.4.0
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder;
using Umbraco.ModelsBuilder.Umbraco;

namespace Umbraco.Web.PublishedContentModels
{
	/// <summary>News overview</summary>
	[PublishedContentModel("NewsOverview")]
	public partial class NewsOverview : PublishedContentModel, INavigationComponent, ITitleComponent
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "NewsOverview";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public NewsOverview(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<NewsOverview, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// News
		///</summary>
		[ImplementPropertyType("newsList")]
		public object NewsList
		{
			get { return this.GetPropertyValue("newsList"); }
		}

		///<summary>
		/// #PropertyHideInNavigation: #PropertyHideInNavigationDesc
		///</summary>
		[ImplementPropertyType("umbracoNaviHide")]
		public bool UmbracoNaviHide
		{
			get { return NavigationComponent.GetUmbracoNaviHide(this); }
		}

		///<summary>
		/// #PropertyTitle: #PropertyTitleDesc
		///</summary>
		[ImplementPropertyType("title")]
		public string Title
		{
			get { return TitleComponent.GetTitle(this); }
		}
	}
}
