;
(function($) {
    'use strict';

    $.fn.tel.Constructor.DEFAULTS.prefixes = [
        { code: '45', iso: 'DK', display: 'Denmark +45', ndd: '' },
        { code: '33', iso: 'FR', display: 'France +33', ndd: '0' },
        { code: '49', iso: 'DE', display: 'Germany +49', ndd: '0' },
        { code: '44', iso: 'GB', display: 'United Kingdom +44', ndd: '0' },
        { code: '1', iso: 'US', display: 'United States +1', ndd: '0' }
    ];

})(jQuery);