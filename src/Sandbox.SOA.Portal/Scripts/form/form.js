;
(function($) {
    "use strict";

    var invalid = "has-error";

    $(function () {

        $("[type='tel']").tel();
        $('select').selectpicker();

        $("body").on(
            "submit", "form[method='POST']",
            function() {

                var $form = $(this),
                    data = $form.serialize();

                $.ajax({
                    url: $form.attr("action"),
                    type: $form.attr("method"),
                    data: data
                })

                    .fail(function (xhr) {
                        var response;

                        $form
                            .find(":input")
                            .popover("destroy")
                            .removeClass(invalid);

                        if (xhr.status === 400) {
                            response = JSON.parse(xhr.responseText);
                            var fields = Object.keys(response);

                            $form.find(".validation-summary")
                                .addClass("alert alert-danger")
                                .empty();

                            fields.forEach(function(field) {
                                if (response[field].Errors && response[field].Errors.length > 0) {
                                    var message = response[field].Errors[0].ErrorMessage,
                                        $field = $form
                                            .find("[name='" + field + "']");

                                    $field
                                        .popover({
                                            content: message,
                                            trigger:"focus",
                                            placement: "auto top",
                                            template: "<div class='popover popover-invalid'><div class='arrow'></div><h3 class='popover-title'></h3><div class='popover-content'></div></div>",
                                            container: $form
                                        })
                                        .addClass(invalid);

                                    var $label = $("<label/>")
                                        .attr("for", $field.attr("id"))
                                        .text(message);

                                    $form.find(".validation-summary")
                                        .append($("<p class='col-sm-offset-2 col-sm-10'/>").append($label));
                                }
                            });
                        }
                    })
                    .done(function(response, status, xhr) {
                        var redirect = xhr.getResponseHeader("redirect");
                        if (redirect) {
                            window.location.href = redirect;

                            return;
                        }

                        $form.find("." + invalid).removeClass('invalid');
                    });

                return false;

            });
    });

})(jQuery);