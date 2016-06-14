/* custom JavaScript code goes here */
$(document).ready(function () {
    $("a").filter(function () {
        return this.innerHTML.indexOf("Delete") == 0;
    }).click(function () {
        return confirm("Are you sure you want to delete this record?");
    });
});