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
	// Mixin content Type 1167 with alias "navigationComponent"
	/// <summary>Navigation</summary>
	public partial interface INavigationComponent : IPublishedContent
	{
		/// <summary>#PropertyHideInNavigation</summary>
		bool UmbracoNaviHide { get; }
	}

	/// <summary>Navigation</summary>
	[PublishedContentModel("navigationComponent")]
	public partial class NavigationComponent : PublishedContentModel, INavigationComponent
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "navigationComponent";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public NavigationComponent(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<NavigationComponent, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// #PropertyHideInNavigation: #PropertyHideInNavigationDesc
		///</summary>
		[ImplementPropertyType("umbracoNaviHide")]
		public bool UmbracoNaviHide
		{
			get { return GetUmbracoNaviHide(this); }
		}

		/// <summary>Static getter for #PropertyHideInNavigation</summary>
		public static bool GetUmbracoNaviHide(INavigationComponent that) { return that.GetPropertyValue<bool>("umbracoNaviHide"); }
	}
}
