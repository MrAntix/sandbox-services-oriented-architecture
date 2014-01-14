;(function($) {
    "use strict";

    $.fn.phone = function () {
        var $this = $(this),
            $prefix = $this.find("select"),
            $number = $this.find("input"),
            $dropdownValue = $("<span class='value'>&nbsp;</span>").css("paddingRight",".5em"),
            $dropdownButton = $("<span class='input-group-addon dropdown-toggle' data-toggle='dropdown'/>")
                .append($dropdownValue)
                .append("<span class='caret'/>"),
            $dropdownList = $("<ul class='dropdown-menu'/>");

        var getBackground = function(value) {
            return value ? "url('/Content/flags/" + value + ".png') no-repeat 0 1px" : "";
        };

        var prefixRegex = /^(\+\d{1,4}\s*(\(\d{1,5}\)\s*)?)?/,
            getPrefix = function (text) {
                return text.replace(/\s+/g, "");
            },
            replacePrefix = function(value, newPrefix) {
                return value.replace(prefixRegex, newPrefix);
            };

        var all = {},
            selected,
            select = function() {
                var $option = $(this),
                    value = "";

                if ($option.length === 0) {

                } else {
                    value = $option.data("value");

                    if($prefix.val()!=value)
                        $number.val(replacePrefix($number.val(), $option.text()));
                }

                $dropdownValue.css({
                    paddingLeft: "17px",
                    background: getBackground(value)
                });
                
                $prefix.val(value);
            };

        $this.find("option").each(function() {
            var $option = $(this),
                optionText = $option.text(),
                $newOption = $("<a href='#'/>")
                    .append(
                        $("<span>")
                            .html(optionText)
                            .css({
                                paddingLeft: "30px",
                                background: getBackground($option.val())
                            })
                    )
                    .data("value", $option.val())
                    .on("click", select);

            var prefix = getPrefix(optionText);
            all[prefix] = $newOption;

            selected =
                $dropdownList.append(
                    $("<li>").append($newOption));

            if ($option.is(":selected"))
                select.call($newOption);
        });

        $this.find(".input-group-addon")
            .replaceWith($dropdownButton);

        $dropdownButton
            .after($dropdownList);

        $number.on("keyup", function() {
            var m = $number.val().match(prefixRegex);
            if (m[0]) {
                var prefix = getPrefix(m[0]);
                select.call(all[prefix]);
            }
        });

    };

})(jQuery);

