;
(function($) {
    'use strict';

    // TEL CLASS DEFINITION
    // ====================

    var Tel = function(element, options) {
        var that = this;

        this.options = options;
        this.prefixes = options.getPrefixes();
        this.$element = $(element);
        this.$button = $('<button class="btn btn-default dropdown-toggle" data-toggle="dropdown"><span class="caret" /></button>');

        var $list = $('<ul class="dropdown-menu"/>'),
            $toggle = $('<div class="input-group-btn"/>')
                .append(this.$button)
                .append($list);

        for (var key in this.prefixes) {
            var prefix = this.prefixes[key],
                $option = $('<a href="#"/>')
                    .append(
                        $('<span/>').text(prefix.display)
                            .css({
                                paddingLeft: '30px',
                                background: getBackground(prefix.value),
                                backgroundPosition: 'left center'
                            })
                    )
                    .data('key', key)
                    .on('click.bs.tel', function() { that.select($(this).data('key')); });

            $list.append(
                $('<li/>').append($option)
            );
        }

        this.$element
            .wrap('<div class="input-group"/>')
            .before($toggle)
            .on("change.bs.tel keyup.bs.tel", function() {
                that.set(that.$element.val());
            });

        this.set(this.$element.val());

    };

    Tel.prototype.select = function(key) {
        /// <summary>Select a prefix given the key</summary>
        var prefix = this.prefixes[key];
        this.$button.css(getButtonCss(prefix));

        if (prefix) {
            var number = this.$element.val(),
                match = number.match(numberMatch.regex);

            this.$element.val(format(key, prefix, match));
        }
    };

    Tel.prototype.set = function(number) {
        /// <summary>Set the number on the control, select and format where matched</summary>
        var match = number.match(numberMatch.regex),
            prefix = null;

        if (match && match[numberMatch.country]) {
            var key = match[numberMatch.country];
            prefix = this.prefixes[key];

            this.$element.val(format(key, prefix, match));
        }

        this.$button.css(getButtonCss(prefix));
    };

    Tel.DEFAULTS = {
        getPrefixes: function() {
            return {
                '45': { value: 'DK', display: 'Denmark +45', ndd: '' },
                '33': { value: 'FR', display: 'France +33', ndd: '0' },
                '49': { value: 'DE', display: 'Germany +49', ndd: '0' },
                '44': { value: 'GB', display: 'United Kingdom +44', ndd: '0' },
                '1': { value: 'US', display: 'United States +1', ndd: '0' }
            };
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

    var getBackground = function(value) {
        return value ? "url('/Content/flags/" + value + ".png') no-repeat" : "";
    };

    var getButtonCss = function(prefix) {
        return prefix
            ? {
                paddingLeft: '3em',
                background: getBackground(prefix.value),
                backgroundPosition: '13px center'
            } : {
                paddingLeft: '3em',
                background: ''
            };
    };

    var format = function(key, prefix, match) {

        var number = '+' + key;

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

})(jQuery);