$("input[data-autocomplete-source]").each(function () {
    var target = $(this);
    target.autocomplete({ source: target.attr("data-autocomplete-source") });
});
$("form[data-ajax]").children().change(function () {
        //alert("Zmieniono");
        $(this).parent().submit();
 
    });
function searchFailed() {
    $("#searchresults").html("Sorry, there was a problem with the search.");
}
function toggleSizes() {
    $("#sizes").toggle("slow");
    $("#sizesUp").toggle();
    $("#sizesDown").toggle();
}
function toggleColors() {
    $("#colors").toggle("slow");
    $("#colorsUp").toggle();
    $("#colorsDown").toggle();
}
$("#sizes").hide();
$("#sizesDown").show();
$("#colors").hide();
$("#colorsDown").show();
function updateUrl() {
    //alert("Wysłano");
    window.history.pushState("", "", $("#url").val());
}
function poczatek() {
    //alert("Poczatek");
}