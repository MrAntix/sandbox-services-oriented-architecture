;
(function($) {
    "use strict";

    var invalid = "has-error";

    $(function() {
        $(".phone").phone();

        $("body").on(
            "submit", "form[method='POST']",
            function() {
                
                var $form = $(this),
                    data = $form.serialize();

                var request = $.ajax({
                    url: $form.attr("action"),
                    type: $form.attr("method"),
                    data: data
                });

                request.fail(function(xhr) {
                    var response;
                    
                    $form.find(".form-group").removeClass(invalid);

if (xhr.status === 400) {
                        response = JSON.parse(xhr.responseText);
                        var fields = Object.keys(response);

                        fields.forEach(function(field) {
                            if (response[field].Errors && response[field].Errors.length > 0) {
                                $form.find("[name='" + field + "']")
                                    .parents('.form-group')
                                    .addClass(invalid);
                            }
                        });
                    }
                });

                request.success(function(data, status, xhr) {
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