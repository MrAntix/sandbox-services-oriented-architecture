;
(function($, document) {
    'use strict';

    // TEL CLASS DEFINITION
    // ====================

    var Tel = function(element, options) {
        var that = this;

        this.options = options;
        this.prefixes = {};
        this.$element = $(element);
        this.$number = $('<input type="tel" class="form-control" />').data('bs.tel', this);
        this.$button = $('<button class="btn btn-default dropdown-toggle" data-toggle="dropdown"><span class="caret" /></button>');

        var $list = $('<ul class="dropdown-menu"/>').css({ maxHeight: "250px", overflowY: "scroll" }),
            $toggle = $('<div class="input-group-btn"/>')
                .append(this.$button)
                .append($list);

        for (var i = 0; i < this.options.prefixes.length; i++) {
            var prefix = this.options.prefixes[i],
                $option = $('<a href="#"/>')
                    .append(
                        $('<span/>').text(prefix.display)
                            .css({
                                paddingLeft: '30px',
                                background: this.options.getButtonBackground(prefix.country),
                                backgroundPosition: 'left center'
                            })
                    )
                    .data('country', prefix.country)
                    .on('click.bs.tel', function () { that.setCountry($(this).data('country')); });

            $list.append(
                $('<li/>').append($option)
            );

            this.prefixes[prefix.country.toUpperCase()] = prefix;
        }

        this.$element
            .wrap('<div class="input-group"/>')
            .before($toggle)
            .after(this.$number
                .on("change.bs.tel keyup.bs.tel", function (e) {
                    console.log(e.keyCode);
                    if (e.keyCode < 48 || e.keyCode > 56) return;

                    that.setNumber(that.$number.val());
                })
            )
            .hide();

        this.setNumber(this.$element.val());

    };

    Tel.prototype.setCountry = function(country) {

        this.set(country, this.number);
        this.$button.css(getButtonCss(this.country, this.options));
    };

    Tel.prototype.setNumber = function(number) {

        this.set(this.country, number);
        this.$button.css(getButtonCss(this.country, this.options));
    };

    Tel.prototype.set = function(country, number) {

        var tel = parse(formatCountry(country) + number, this.options.prefixes);
        this.$element.val(format(tel));

        this.country = tel.country;

        tel.country = null;
        this.number = format(tel);

        if (caretIsLast(this.$number[0]))
            this.$number.val(this.number);
    };

    Tel.DEFAULTS = {
        prefixes: {},
        getButtonBackground: function(country) {
            return country ? "url('/Content/flags/" + country + ".png') no-repeat" : "";
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

    $.fn.tel.format = function(value, prefixes) {

        var tel = parse(value, prefixes || Tel.DEFAULTS.prefixes);

        return format(tel);
    };

    // TEL NO CONFLICT
    // ===============

    $.fn.tel.noConflict = function() {
        $.fn.tel = old;
        return this;
    };

    var numberMatch = {
        regex: /^(\[([a-zA-Z]{2})\]\s*)?\+?(\d{1,4})?\s*?(\s|\s?\((\d{0,4})\))?\s*?(\s)?([\-\.\s\d]{1,15})?$/,
        country: 2,
        code: 3,
        nddContainer: 4,
        ndd: 5,
        localSpace: 6,
        local: 7
    };

    var getButtonCss = function (country, options) {
        if (!options) throw "getButtonCss(): options required";

        return country
            ? {
                paddingLeft: '3em',
                background: options.getButtonBackground(country),
                backgroundPosition: '13px center'
            } : {
                paddingLeft: '3em',
                background: ''
            };
    };

    var parse = function(value, prefixes) {
        if (value) {
            var match = value.toUpperCase().match(numberMatch.regex);

            if (match) {

                var country = match[numberMatch.country] || '',
                    code = match[numberMatch.code] || '',
                    ndd = match[numberMatch.ndd] || '',
                    local = match[numberMatch.local] || '',
                    prefix = prefixBy('country', country, prefixes)
                        || prefixBy('code', code, prefixes);

                if (prefix) {
                    country = country || prefix.country;
                    code = prefix.code;

                    if (prefix.ndd) {
                        if (!ndd
                            && local.length > prefix.ndd.length
                            && local.substr(0, prefix.ndd.length) === prefix.ndd) {
                            ndd = prefix.ndd;
                            local = local.substr(ndd.length);
                        }

                    } else {
                        local = ndd + local;
                        ndd = '';
                    }
                }

                return {
                    country: country,
                    code: code,
                    ndd: ndd,
                    local: local
                };
            }

            return {
                local: value
            };
        }

        return value;
    };

    var format = function(tel) {

        var a = [
            formatCountry(tel.country),
            formatCode(tel.code),
            formatNdd(tel.ndd),
            tel.local
        ];

        return $.grep(a,
            function(i) { return i; })
            .join(' ');
    },
        formatCountry = function(value) {
            return value ? '[' + value.toUpperCase() + ']' : '';
        },
        formatCode = function(value) {
            return value ? '+' + value : '';
        },
        formatNdd = function(value) {
            return value ? '(' + value + ')' : '';
        };

    var prefixBy = function(propertyName, value, prefixes) {
        if (!value) return null;
        var matches = $.grep(prefixes, function(prefix) {
            return prefix[propertyName] == value;
        });
        return matches.length === 1 ? matches[0] : null;
    };

    var caretIsLast = function (el) {

        var valueLength = el.value.length;

        if (el.selectionStart !== undefined)
            return el.selectionStart === valueLength;

        if (document.selection) {

            var sel = document.selection.createRange();
            sel.moveStart('character', -valueLength);

            return sel.text.length === valueLength;
        }

        throw "not supported";
    };

})(jQuery, document);