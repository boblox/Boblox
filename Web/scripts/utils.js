function showDialog(content) {
    var dialog = $("<p>").text(content);
    $(dialog).dialog({
        buttons: {
            "Згода": function () {
                $(this).dialog("close");
            }
        }
    });
}

function reloadDisqusCommentsCounter() {
    window.DISQUSWIDGETS = undefined;
    $.getScript("http://" + config.disqus_shortname + ".disqus.com/count.js");
}

var config = {
    disqusShortName: "boblox",
    newsStartPage: 1,
    galleryStartPage: 1,
    yearAllInt: 0
}