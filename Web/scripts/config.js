/*********************************************=ImagesCarousel=****************************************************************/
function InitImagesCarousel(carouselWrapperId) {
    var carouselWrapper = $("#" + carouselWrapperId);
    var carousel = carouselWrapper.find('.owl-carousel');
    carousel.owlCarousel({
        items: 1,
        singleItem: true,
        lazyLoad: true,
        transitionStyle: "fade"
    });

    //new next-prev buttons
    //carouselWrapper.find('.owl-carousel-prev').click(function () {
    //    carousel.trigger('prev.owl.carousel', [800]);
    //});
    //carouselWrapper.find('.owl-carousel-next').click(function () {
    //    carousel.trigger('next.owl.carousel', [800]);
    //});
}

/*********************************************=ImagesGallery=****************************************************************/
function InitImagesGallery(carouselWrapperId1, carouselWrapperId2) {
    var carouselWrapper1 = $("#" + carouselWrapperId1);
    var carouselWrapper2 = $("#" + carouselWrapperId2);
    var carousel1 = carouselWrapper1.find(".owl-carousel");
    var carousel2 = carouselWrapper2.find(".owl-carousel");
    carousel1.owlCarousel({
        items: 1,
        singleItem: true,
        lazyLoad: true,
        pagination: false,
        transitionStyle: "fade",
        autoHeight: true,
        //theme: '',
        afterAction: syncPosition
    });

    carousel2.owlCarousel({
        items: 5,
        lazyLoad: true,
        pagination: false,
        //navigation: true,
        transitionStyle: "fade",
        responsiveRefreshRate: 100,
        //theme: '',
        afterInit: function (el) {
            setSelected(0);
        }
    });

    function setSelected(number) {
        carousel2.find(".owl-item").removeClass("selected")
            .eq(number).addClass("selected");
    }

    function syncPosition(el) {
        var current = this.currentItem;
        setSelected(current);
        if (carousel2.data("owlCarousel") !== undefined) {
            center(current);
        }
    }

    carousel2.on("click", ".owl-item", function (e) {
        e.preventDefault();
        var number = $(this).data("owlItem");
        carousel1.trigger("owl.goTo", number);
    });

    function center(number) {
        var sync2Visible = carousel2.data("owlCarousel").owl.visibleItems;
        var num = number;
        var found = false;
        for (var i in sync2Visible) {
            if (num === sync2Visible[i]) {
                found = true;
            }
        }

        if (found === false) {
            if (num > sync2Visible[sync2Visible.length - 1]) {
                carousel2.trigger("owl.goTo", num - sync2Visible.length + 2);
            } else {
                if (num - 1 === -1) {
                    num = 0;
                }
                carousel2.trigger("owl.goTo", num);
            }
        } else if (num === sync2Visible[sync2Visible.length - 1]) {
            carousel2.trigger("owl.goTo", sync2Visible[1]);
        } else if (num === sync2Visible[0]) {
            carousel2.trigger("owl.goTo", num - 1);
        }
    }

    //new next-prev buttons
    carouselWrapper2.find('.owl-carousel-prev').click(function () {
        carousel2.trigger('owl.prev', [800]);
    });
    carouselWrapper2.find('.owl-carousel-next').click(function () {
        carousel2.trigger('owl.next', [800]);
    });
}

/*********************************************=Banner Carousel=***********************************************************/
function InitBannerCarousel(carouselWrapperId) {
    var carouselWrapper = $("#" + carouselWrapperId);
    var carousel = carouselWrapper.find('.owl-carousel');
    carousel.owlCarousel({
        items: 1,
        singleItem: true,
        lazyLoad: true,
        autoPlay: 4500,
        stopOnHover: true,
        transitionStyle: "fade"
    });

    //new next-prev buttons
    //carouselWrapper.find('.owl-carousel-prev').click(function () {
    //    carousel.trigger('prev.owl.carousel', [800]);
    //});
    //carouselWrapper.find('.owl-carousel-next').click(function () {
    //    carousel.trigger('next.owl.carousel', [800]);
    //});
}

/*********************************************=Articles Pager=***********************************************************/
function InitArticlesPager(dataAttr, routingObj) {
    var links = $(".articles-pager a[" + dataAttr + "]");

    routingObj.addRouteChangeCallback(function (route) {
        links.removeClass('active');
        links.filter("[data-page=" + route.page + "]").addClass('active');
    });

    links.each(function (index, element) {
        $(element).off('click');
        $(element).on('click', function (e) {
            e.preventDefault();
            routingObj.updateRouteAndHash({ page: $(this).data('page') });
        });
    });
}

/*********************************************=Years List=***********************************************************/
function InitYearsList(dataAttr, routingObj) {
    var select = $("select.years-list[" + dataAttr + "]");

    routingObj.addRouteChangeCallback(function (route) {
        select.find("option[data-year=" + route.year + "]").attr("selected", "true");
        select.selectpicker("refresh");
    });

    select.on('change', function (e) {
        var selected = $(this).find("option:selected");
        routingObj.updateRouteAndHash({ year: selected.data('year') });
    });
}

/*********************************************=News Overview Routing=*****************************************************/
var newsRouting = (function () {
    var callbacks = [];
    var containerId, overlayId, itemsPerPage, yearAllInt;
    var currentRoute, routes, router;

    function getInitRoute() {
        var route = {
            year: yearAllInt,
            page: config.newsStartPage,
            itemsPerPage: itemsPerPage
        };
        return route;
    }

    function updateRouteAndHash(newRouteData) {
        var year = newRouteData.year;
        var page = newRouteData.page;
        currentRoute.year = year === undefined ? currentRoute.year : year;

        if (year !== undefined) {
            //reset news feed page if we clicked on category or year
            currentRoute.page = config.newsStartPage;
        }
        else {
            currentRoute.page = page === undefined ? currentRoute.page : page;
        }

        var hash = 'year/' + encodeURIComponent(currentRoute.year) +
            '/page/' + encodeURIComponent(currentRoute.page);
        router.setRoute(hash);
    }

    function dealWithRoute(route) {
        $(callbacks).each(function (index, element) {
            element(route);
        });

        var list = $("#" + containerId);
        //overlay shown over list of items when news are loaded
        var loadingOverlay = list.siblings("#" + overlayId).show();
        $.ajax({
            url: '/umbraco/surface/News/Index/',
            type: 'POST',
            dataType: 'html',
            data: JSON.stringify(route),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                list.fadeOut("fast", function () {
                    loadingOverlay.hide();
                    list.html(data).fadeIn("fast");
                    reloadDisqusCommentsCounter();
                });
            },
            error: function (request, status) {
                loadingOverlay.hide();
                showDialog("Вибачте, неможливо завантажити новини");
            }
        });
    }

    function addRouteChangeCallback(callback) {
        callbacks.push(callback);
        if (currentRoute !== undefined) {
            callback(currentRoute);
        }
    }

    function init(data) {
        containerId = data.containerId;
        overlayId = data.overlayId;
        itemsPerPage = data.itemsPerPage || 1;
        yearAllInt = data.yearAllInt || 0;
        router.init("/");

        //Routing should work only on NewsOverview page
        //router.init('/');
        //var hash = window.location.hash.slice(2);
        //router.setRoute('/');
        //router.setRoute(hash);
    }

    routes = {
        '/year/:year/page/:page':
            function (year, page) {
                currentRoute = {
                    year: decodeURIComponent(year),
                    page: decodeURIComponent(page),
                    itemsPerPage: itemsPerPage
                };
                dealWithRoute(currentRoute);
            },
        '/': function () {
            currentRoute = getInitRoute();
            dealWithRoute(currentRoute);
        }
    };
    router = Router(routes);

    return {
        updateRouteAndHash: updateRouteAndHash,
        init: init,
        addRouteChangeCallback: addRouteChangeCallback
    }
})();

/*********************************************=Gallery Overview routing=***************************************************/
var galleryRouting = (function () {
    var callbacks = [];
    var containerId, overlayId, itemsPerPage, yearAllInt;
    var currentRoute, routes, router;

    function getInitRoute() {
        var route = {
            year: yearAllInt,
            page: config.galleryStartPage,
            itemsPerPage: itemsPerPage
        };
        return route;
    }

    function updateRouteAndHash(newRouteData) {
        var year = newRouteData.year;
        var page = newRouteData.page;
        currentRoute.year = year === undefined ? currentRoute.year : year;

        if (year !== undefined) {
            //reset gallery feed page if we clicked on category or year
            currentRoute.page = config.galleryStartPage;
        }
        else {
            currentRoute.page = page === undefined ? currentRoute.page : page;
        }

        var hash = 'year/' + encodeURIComponent(currentRoute.year) +
            '/page/' + encodeURIComponent(currentRoute.page);
        router.setRoute(hash);
    }

    function dealWithRoute(route) {
        $(callbacks).each(function (index, element) {
            element(route);
        });

        var list = $("#" + containerId);
        //overlay shown over list of items when news are loaded
        var loadingOverlay = list.siblings("#" + overlayId).show();
        $.ajax({
            url: '/umbraco/surface/Gallery/Index/',
            type: 'POST',
            dataType: 'html',
            data: JSON.stringify(route),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                list.fadeOut("fast", function () {
                    loadingOverlay.hide();
                    list.html(data).fadeIn("fast");
                    //reloadDisqusCommentsCounter();
                });
            },
            error: function (request, status) {
                loadingOverlay.hide();
                showDialog("Вибачте, неможливо завантажити галерею");
            }
        });
    }

    function addRouteChangeCallback(callback) {
        callbacks.push(callback);
        if (currentRoute !== undefined) {
            callback(currentRoute);
        }
    }

    function init(data) {
        containerId = data.containerId;
        overlayId = data.overlayId;
        itemsPerPage = data.itemsPerPage || 1;
        yearAllInt = data.yearAllInt || 0;
        router.init("/");

        //Routing should work only on NewsOverview page
        //router.init('/');
        //var hash = window.location.hash.slice(2);
        //router.setRoute('/');
        //router.setRoute(hash);
    }

    routes = {
        '/year/:year/page/:page':
            function (year, page) {
                currentRoute = {
                    year: decodeURIComponent(year),
                    page: decodeURIComponent(page),
                    itemsPerPage: itemsPerPage
                };
                dealWithRoute(currentRoute);
            },
        '/': function () {
            currentRoute = getInitRoute();
            dealWithRoute(currentRoute);
        }
    };
    router = Router(routes);

    return {
        updateRouteAndHash: updateRouteAndHash,
        init: init,
        addRouteChangeCallback: addRouteChangeCallback
    }
})();

/*********************************************=Gallery Overview routing=***************************************************/
function InitGalleryItem(id) {
    $("#" + id + " .image").magnificPopup({
        type: 'image',
        //delegate: 'img',
        image: {
            titleSrc: null,
            cursor: null
        },
        gallery: {
            enabled: true,
            preload: [0, 1],
            arrowMarkup: '<button title="%title%" type="button" class="mfp-custom-arrow arrow-%dir%">' +
                            '<div class="mfp-prevent-close"></div>' +
                         '</button>'
        },
        tLoading: '',
        closeMarkup: '<button title="%title%" class="mfp-close">' +
                    '</button>',
        mainClass: 'mfp-fade',
        removalDelay: 300,
        callbacks: {
            buildControls: function () {
                // re-appends controls inside the main container
                this.contentContainer.append(this.arrowLeft.add(this.arrowRight));
            }
        }
    });
}

/***************************************************=Disqus=******************************************************************/
function InitDisqus(identifier) {
    /* * * CONFIGURATION VARIABLES: EDIT BEFORE PASTING INTO YOUR WEBPAGE * * */
    window.disqus_shortname = config.disqusShortName; // required: replace example with your forum shortname
    window.disqus_identifier = identifier;
    /* * * DON'T EDIT BELOW THIS LINE * * */
    (function () {
        var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
        dsq.src = '//' + disqus_shortname + '.disqus.com/embed.js';
        (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
    })();
}

/***************************************************=Google Map=**************************************************************/
function InitGoogleMap(settings) {
    var position = { lat: settings.latitude, lng: settings.longitude };
    var el = $("#" + settings.id).get(0);
    var map = new google.maps.Map(el, {
        zoom: settings.zoom,
        center: position
        //scrollwheel: false,
    });

    // Create a marker and set its position.
    new google.maps.Marker({
        map: map,
        position: position,
        title: settings.title
    });
}

/**********************************************=Scroll to top=***************************************************************/
function InitScrollToTop(identifier) {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 150) {
            $(identifier).fadeIn("slow");
        } else {
            $(identifier).fadeOut("slow");
        }
    });

    //Click event to scroll to top
    $(identifier).click(function () {
        $('html, body').animate({ scrollTop: 0 }, 800);
        return false;
    });
}

/**********************************************=Loading spinner=***************************************************************/
function InitLoadingSpinner(identifier) {
    var $loading = $(identifier).hide();
    $(document)
      .ajaxStart(function () {
          $loading.fadeIn();
      })
      .ajaxStop(function () {
          $loading.fadeOut();
      });
}

/**********************************************=Collapsible header=***********************************************************/
function InitCollapsibleHeader(linkId, rteId) {
    var rte = $("#" + rteId);
    var link = $("#" + linkId);
    link.on("click", function () {
        rte.slideToggle();
        link.find("span.glyphicon").toggleClass("glyphicon-triangle-right glyphicon-triangle-bottom");
    });
    rte.hide();
}

/*****************************************Google analytics******************************************************************/
(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments);
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g;
    m.parentNode.insertBefore(a, m);
})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

ga('create', 'UA-54105375-5', 'auto');
ga('send', 'pageview');
/*End of Google analytics*/