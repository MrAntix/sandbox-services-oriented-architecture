;
(function($) {
    'use strict';

    // TEL CLASS DEFINITION
    // ====================

    var Tel = function(element, options) {
        var that = this;

        this.options = options;
        this.prefixes = {};
        this.$element = $(element);
        this.$button = $('<button class="btn btn-default dropdown-toggle" data-toggle="dropdown"><span class="caret" /></button>');

        var $list = $('<ul class="dropdown-menu"/>'),
            $toggle = $('<div class="input-group-btn"/>')
                .append(this.$button)
                .append($list);

        for (var i = 0; i < this.options.prefixes.length;i++) {
            var prefix = this.options.prefixes[i],
                $option = $('<a href="#"/>')
                    .append(
                        $('<span/>').text(prefix.display)
                            .css({
                                paddingLeft: '30px',
                                background: this.options.getButtonBackground(prefix.iso),
                                backgroundPosition: 'left center'
                            })
                    )
                    .data('code', prefix.code)
                    .on('click.bs.tel', function () { that.select($(this).data('code')); });

            $list.append(
                $('<li/>').append($option)
            );

            this.prefixes[prefix.code] = prefix;
        }

        this.$element
            .wrap('<div class="input-group"/>')
            .before($toggle)
            .on("change.bs.tel keyup.bs.tel", function(e) {
                if ($.inArray(e.keyCode,[8, 46])!=-1) return;

                that.set(that.$element.val());
            });

        this.set(this.$element.val());

    };

    Tel.prototype.select = function(code) {
        /// <summary>Select a prefix given the code</summary>
        var prefix = this.prefixes[code];
        this.$button.css(getButtonCss(prefix, this.options));

        if (prefix) {
            var number = this.$element.val(),
                match = number.match(numberMatch.regex);

            setValue(this.$element, format(code, prefix, match));
        }
    };

    Tel.prototype.set = function(number) {
        /// <summary>Set the number on the control, select and format where matched</summary>
        var match = number.match(numberMatch.regex),
            prefix = null;

        if (match && match[numberMatch.country]) {
            var code = match[numberMatch.country];
            prefix = this.prefixes[code];

            setValue(this.$element, format(code, prefix, match));
        }

        this.$button.css(getButtonCss(prefix, this.options));
    };

    Tel.DEFAULTS = {
        prefixes: {},
        getButtonBackground: function (iso) {
            return iso ? "url('/Content/flags/" + iso + ".png') no-repeat" : "";
        }
    };

    // TEL PLUGIN DEFINITION
    // =====================

    var old = $.fn.tel;

    $.fn.tel = function(option) {
        return this.each(function() {
            var $this = $(this);
            var options = $.extend({}, Tel.DEFAULTS, $this.data(), typeof option == 'object' && option);
            var data = $this.data('bs.tel');

            if (!data) $this.data('bs.tel', (data = new Tel(this, options)));
            if (typeof option == 'string') data[option].call($this);
        });
    };

    $.fn.tel.Constructor = Tel;

    // TEL NO CONFLICT
    // ===============

    $.fn.tel.noConflict = function() {
        $.fn.tel = old;
        return this;
    };

    var numberMatch = {
        regex: /^\+?(\d{1,4})\s*?(\s|\s?\((\d{1,4})\))\s*?(\s)?([\-\.\s\d]{1,15})?$/,
        country: 1,
        nddContainer: 2,
        ndd: 3,
        localSpace: 4,
        local: 5
    };

    var getButtonCss = function(prefix, options) {
        return prefix
            ? {
                paddingLeft: '3em',
                background: options.getButtonBackground(prefix.iso),
                backgroundPosition: '13px center'
            } : {
                paddingLeft: '3em',
                background: ''
            };
    };

    var format = function (code, prefix, match) {

        var number = '+' + code;

        if (match) {
            var local = match[numberMatch.local],
                ndd = match[numberMatch.ndd];

            if (local) {
                if (ndd == null
                    && local.length > prefix.ndd.length
                    && local.substr(0, prefix.ndd.length) === prefix.ndd) {
                    ndd = prefix.ndd;
                    local = local.substr(ndd.length);
                }

                if (prefix.ndd) {
                    number += formatNdd(ndd);
                } else {
                    local = ndd + local;
                }

                number += ' ' + local;

            } else {
                if (match[numberMatch.nddContainer]) number += match[numberMatch.nddContainer];
                number += (match[numberMatch.localSpace] || '');

            }
        } else {
            number += formatNdd(prefix.ndd);
        }

        return number;
    },
        formatNdd = function(ndd) {
            return ndd ? ' (' + ndd + ')' : '';
        };

    var setValue = function($el, value) {
        if ($el.val() != value) $el.val(value);
    };
    
})(jQuery);