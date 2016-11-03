[
    {
        "name": "Редактор тексту",
        "alias": "rte",
        "view": "rte",
        "icon": "icon-article"
    },
    {
        "name": "Світлина",
        "alias": "media",
        "view": "media",
        "icon": "icon-picture"
    },
    {
        "name": "Макро",
        "alias": "macro",
        "view": "macro",
        "icon": "icon-settings-alt"
    },
    {
        "name": "Відео",
        "alias": "embed",
        "view": "embed",
        "icon": "icon-movie-alt"
    },
    {
        "name": "Заголовок: маленький",
        "alias": "headline-small",
        "view": "textstring",
        "icon": "icon-forms-paypal",
        "config": {
            "style": "font-size: 18px; line-height: 1.1; font-weight:bold;",
            "markup": "<h4>#value#</h4>"
        }
    },
    {
        "name": "Заголовок: стандартний",
        "alias": "headline-normal",
        "view": "textstring",
        "icon": "icon-forms-paypal",
        "config": {
            "style": "font-size: 22px; line-height: 1.1; font-weight:bold;",
            "markup": "<h3>#value#</h3>"
        }
    },
    {
        "name": "Заголовок: великий",
        "alias": "headline-big",
        "view": "textstring",
        "icon": "icon-forms-paypal",
        "config": {
            "style": "font-size: 35px; color:#f6ab32; line-height: 1.1; font-weight:bold;",
            "markup": "<h2>#value#</h2>"
        }
    },
    {
        "name": "Цитата",
        "alias": "quote",
        "view": "textstring",
        "icon": "icon-quote",
        "config": {
            "style": "border-left: 3px solid #ccc; padding: 10px; color: #ccc; font-family: serif; font-variant: italic; font-size: 18px",
            "markup": "<blockquote>#value#</blockquote>"
        }
    },
    {
        "name": "Карусель світлин",
        "alias": "Carousel",
        "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
        "icon": "icon-layers-alt",
        "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
        "config": {
            "editors": [
                {
                    "name": "Images",
                    "alias": "images",
                    "propretyType": {},
                    "dataType": "7e3962cc-ce20-4ffc-b661-5897a894ba7e"
                }
            ],
            "renderInGrid": "1",
            "frontView": "",
            "max": null
        }
    },
    {
        "name": "Галерея світлин",
        "alias": "Gallery",
        "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
        "icon": "icon-layers-alt",
        "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
        "config": {
            "editors": [
                {
                    "name": "Images",
                    "alias": "images",
                    "propretyType": {},
                    "dataType": "7e3962cc-ce20-4ffc-b661-5897a894ba7e"
                }
            ],
            "renderInGrid": "1",
            "frontView": "",
            "max": null
        }
    },
    {
        "name": "Текст з вкладками",
        "alias": "tabbedRte",
        "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
        "icon": "icon-article",
        "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
        "config": {
            "editors": [
                {
                    "name": "Title",
                    "alias": "title",
                    "propretyType": {},
                    "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
                },
                {
                    "name": "Content",
                    "alias": "content",
                    "propretyType": {},
                    "dataType": "ca90c950-0aff-4e72-b976-a30b1ac57dad"
                }
            ],
            "renderInGrid": "0",
            "frontView": "",
            "max": 5
        }
    }
]