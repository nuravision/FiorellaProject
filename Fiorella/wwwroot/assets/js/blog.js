$(function () {
    $(document).on("click", ".show-more button", function () {
        let skip = parseInt($(".blogs-area").children().length);
        let blogsCount = parseInt($(".blogs-area").attr("data-count"));
        let parentElem = $(".blogs-area");
        $.ajax({
            url: `blog/showmore?skip=${skip}`,
            type: "GET",
            success: function (response) {
                $(parentElem).append(response);
                skip = parseInt($(".blogs-area").children().length);
                if (skip >= blogsCount) {
                    $(".show-more button").addClass("d-none");
                }
            },
            error: function (xhr, status, error) {
                console.error("AJAX Error:", error);
            }
        });
    });
});