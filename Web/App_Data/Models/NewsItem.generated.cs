//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.2.93
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
	/// <summary>News item</summary>
	[PublishedContentModel("NewsItem")]
	public partial class NewsItem : PublishedContentModel, ILocationComponent, ISubtitleComponent, ITitleComponent
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "NewsItem";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public NewsItem(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<NewsItem, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Content
		///</summary>
		[ImplementPropertyType("content")]
		public Newtonsoft.Json.Linq.JToken Content
		{
			get { return this.GetPropertyValue<Newtonsoft.Json.Linq.JToken>("content"); }
		}

		///<summary>
		/// Image
		///</summary>
		[ImplementPropertyType("image")]
		public Umbraco.Web.Models.ImageCropDataSet Image
		{
			get { return this.GetPropertyValue<Umbraco.Web.Models.ImageCropDataSet>("image"); }
		}

		///<summary>
		/// Publish date: You can overrule the date shown on the site by changing it here. If you don't set a date explicitly here then the news item's create date will be shown.
		///</summary>
		[ImplementPropertyType("publishDate")]
		public DateTime PublishDate
		{
			get { return this.GetPropertyValue<DateTime>("publishDate"); }
		}

		///<summary>
		/// Tags
		///</summary>
		[ImplementPropertyType("tags")]
		public object Tags
		{
			get { return this.GetPropertyValue("tags"); }
		}

		///<summary>
		/// Place
		///</summary>
		[ImplementPropertyType("place")]
		public string Place
		{
			get { return LocationComponent.GetPlace(this); }
		}

		///<summary>
		/// #PropertySubtitle: #PropertySubtitleDesc
		///</summary>
		[ImplementPropertyType("subtitle")]
		public string Subtitle
		{
			get { return SubtitleComponent.GetSubtitle(this); }
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
