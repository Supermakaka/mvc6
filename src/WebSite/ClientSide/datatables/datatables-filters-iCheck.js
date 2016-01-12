/**
Support for iCheck
*/

var dtFilters = require('datatables-filters');
require('iCheck');

$(document).on('init.dt', function (e, settings, json) {
    bindHandlers($(settings.nTable));
});

function bindHandlers(table) {
    dtFilters.getFiltersByTable(table).on('ifToggled', function (e) {
        dtFilters.applyFilter($(this), true);
    });
}