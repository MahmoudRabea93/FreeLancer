﻿/*! http://keith-wood.name/datepick.html
	Datepicker extensions for jQuery v5.1.1.
	Written by Keith Wood (wood.keith{at}optusnet.com.au) August 2009.
	Licensed under the MIT (http://keith-wood.name/licence.html) licence.
	Please attribute the author if you use it. */

(function($) { // Hide scope, no $ conflict
	'use strict';

	var themeRollerRenderer = {
		picker: '<div{popup:start} id="ui-datepicker-div"{popup:end} class="ui-datepicker ui-widget ' +
		'ui-widget-content ui-helper-clearfix ui-corner-all{inline:start} ui-datepicker-inline{inline:end}">' +
		'<div class="ui-datepicker-header ui-widget-header ui-helper-clearfix ui-corner-all">' +
		'{link:prev}{link:today}{link:next}</div>{months}' +
		'{popup:start}<div class="ui-datepicker-header ui-widget-header ui-helper-clearfix ' +
		'ui-corner-all">{button:clear}{button:close}</div>{popup:end}' +
		'<div class="ui-helper-clearfix"></div></div>',
		monthRow: '<div class="ui-datepicker-row-break">{months}</div>',
		month: '<div class="ui-datepicker-group">' +
		'<div class="ui-datepicker-header ui-widget-header ui-helper-clearfix ui-corner-all">{monthHeader:MM yyyy}</div>' +
		'<table class="ui-datepicker-calendar"><thead>{weekHeader}</thead><tbody>{weeks}</tbody></table></div>',
		weekHeader: '<tr>{days}</tr>',
		dayHeader: '<th>{day}</th>',
		week: '<tr>{days}</tr>',
		day: '<td>{day}</td>',
		monthSelector: '.ui-datepicker-group',
		daySelector: 'td',
		rtlclass: 'ui-datepicker-rtl',
		multiclass: 'ui-datepicker-multi',
		defaultclass: 'ui-state-default',
		selectedclass: 'ui-state-active',
		highlightedclass: 'ui-state-hover',
		todayclass: 'ui-state-highlight',
		otherMonthclass: 'ui-datepicker-other-month',
		weekendclass: 'ui-datepicker-week-end',
		commandclass: 'ui-datepicker-cmd',
		commandButtonclass: 'ui-state-default ui-corner-all',
		commandLinkclass: '',
		disabledclass: 'ui-datepicker-disabled'
	};

	/** Extensions to the {@linkcode module:Datepick|Datepick} plugin.
		@module Datepick-ext */
	$.extend($.datepick, {

		/** Template for generating a datepicker showing week of year.
			Use it with the {@linkcode module:Datepick~regionalOptions|renderer} option.
			@example renderer: $.datepick.weekOfYearRenderer */
		weekOfYearRenderer: $.extend({}, $.datepick.defaultRenderer, {
			weekHeader: '<tr><th class="datepick-week">' +
			'<span title="{l10n:weekStatus}">{l10n:weekText}</span></th>{days}</tr>',
			week: '<tr><td class="datepick-week">{weekOfYear}</td>{days}</tr>'
		}),

		/** ThemeRoller template for generating a datepicker.
			Use it with the {@linkcode module:Datepick~regionalOptions|renderer} option.
			@example renderer: $.datepick.themeRollerRenderer */
		themeRollerRenderer: themeRollerRenderer,

		/** ThemeRoller template for generating a datepicker showing week of year.
			Use it with the {@linkcode module:Datepick~regionalOptions|renderer} option.
			@example renderer: $.datepick.themeRollerWeekOfYearRenderer */
		themeRollerWeekOfYearRenderer: $.extend({}, themeRollerRenderer, {
			weekHeader: '<tr><th class="ui-state-hover"><span>{l10n:weekText}</span></th>{days}</tr>',
			week: '<tr><td class="ui-state-hover">{weekOfYear}</td>{days}</tr>'
		}),

		/** Don't allow weekends to be selected.
			Use it with the {@linkcode module:Datepick~defaultOptions|onDate} option.
			@param {Date} date The current date.
			@return {object} Information about this date.
			@example onDate: $.datepick.noWeekends */
		noWeekends: function(date) {
			return {selectable: (date.getDay() || 7) < 6};
		},

		/** Change the first day of the week by clicking on the day header.
			Use it with the {@linkcode module:Datepick~defaultOptions|onShow} option.
			@param {jQuery} picker The completed datepicker division.
			@param {object} inst The current instance settings.
			@example onShow: $.datepick.changeFirstDay */
		changeFirstDay: function(picker, inst) { // jshint unused:false
			var target = $(this);
			picker.find('th span').each(function() {
				var parent = $(this).parent();
				if (parent.is('.datepick-week') || parent.is('.ui-state-hover')) {
					return;
				}
				$('<a href="#" class="' + this.className +
						'" title="Change first day of the week">' + $(this).text() + '</a>').
					click(function() {
						var dow = parseInt(this.className.replace(/^.*datepick-dow-(\d+).*$/, '$1'), 10);
						target.datepick('option', {firstDay: dow});
						return false;
					}).
					replaceAll(this);
			});
		},

		/** Callback when hovering over a date within the datepicker.
			Use it with the {@linkcode module:Datepick-ext~hoverCallback|hoverCallback} function.
			@callback DatepickOnHover
			@global
			@param {Date} date The currently hovered date.
			@param {boolean} selectable <code>true</code> if this date can be selected.
			@example $.datepick.hoverCallback(function(date, selectable) {
  $('#hoverOutput').text(
    (selectable ? $.datepick.formatDate(date) : null) || 'nothing');
}) */

		/** Add a callback when hovering over dates.
			Use it with the {@linkcode module:Datepick~defaultOptions|onShow} option.
			@param {DatepickOnHover} onHover The callback when hovering, it receives the current date and
						a flag indicating selectability as parameters on entry,
						and no parameters on exit, <code>this</code> refers to the target input or division.
			@example onShow: $.datepick.hoverCallback(handleHover) */
		hoverCallback: function(onHover) {
			return function(picker, inst) {
				var target = this;
				var renderer = inst.get('renderer');
				picker.find(renderer.daySelector + ' a, ' + renderer.daySelector + ' span').
					hover(function() {
						onHover.apply(target, [$.datepick.retrieveDate(target, this),
							this.nodeName.toLowerCase() === 'a']);
					},
					function() { onHover.apply(target, []); });
			};
		},

		/** Highlight the entire week when hovering over it.
			Use it with the {@linkcode module:Datepick~defaultOptions|onShow} option.
			@param {jQuery} picker The completed datepicker division.
			@param {object} inst The current instance settings.
			@example onShow: $.datepick.highlightWeek */
		highlightWeek: function(picker, inst) { // jshint unused:false
			var target = this;
			var renderer = inst.get('renderer');
			picker.find(renderer.daySelector + ' a, ' + renderer.daySelector + ' span').
				hover(function() {
					$(this).parents('tr').find(renderer.daySelector + ' *').
						addclass(renderer.highlightedclass);
				},
				function() {
					$(this).parents('tr').find(renderer.daySelector + ' *').
						removeclass(renderer.highlightedclass);
				});
		},

		/** Show a status bar with messages.
			Use it with the {@linkcode module:Datepick~defaultOptions|onShow} option.
			@param {jQuery} picker The completed datepicker division.
			@param {object} inst The current instance settings.
			@example onShow: $.datepick.showStatus */
		showStatus: function(picker, inst) {
			var renderer = inst.get('renderer');
			var isTR = (renderer.selectedclass === themeRollerRenderer.selectedclass);
			var defaultStatus = inst.get('defaultStatus') || '&#160;';
			var status = $('<div class="' + (!isTR ? 'datepick-status' :
				'ui-datepicker-status ui-widget-header ui-helper-clearfix ui-corner-all') + '">' +
				defaultStatus + '</div>').
				insertAfter(picker.find('.datepick-month-row:last,.ui-datepicker-row-break:last'));
			picker.find('*[title]').each(function() {
					var title = $(this).attr('title');
					$(this).removeAttr('title').hover(
						function() { status.text(title || defaultStatus); },
						function() { status.text(defaultStatus); });
				});
		},

		/** Allow easier navigation by month/year.
			Use it with the {@linkcode module:Datepick~defaultOptions|onShow} option.
			@param {jQuery} picker The completed datepicker division.
			@param {object} inst The current instance settings.
			@example onShow: $.datepick.monthNavigation */
		monthNavigation: function(picker, inst) {
			var target = $(this);
			var renderer = inst.get('renderer');
			var isTR = (renderer.selectedclass === themeRollerRenderer.selectedclass);
			var minDate = inst.curMinDate();
			var maxDate = inst.get('maxDate');
			var monthNames = inst.get('monthNames');
			var monthNamesShort = inst.get('monthNamesShort');
			var month = inst.drawDate.getMonth();
			var year = inst.drawDate.getFullYear();
			var inRange = false;
			var html = '<div class="' + (!isTR ? 'datepick-month-nav' : 'ui-datepicker-month-nav') + '"' +
				' style="display: none;">';
			for (var i = 0; i < monthNames.length; i++) {
				inRange = ((!minDate || new Date(year, i + 1, 0).getTime() >= minDate.getTime()) &&
					(!maxDate || new Date(year, i, 1).getTime() <= maxDate.getTime()));
				html += '<div>' +
					(inRange ? '<a href="#" class="dp' + new Date(year, i, 1).getTime() + '"' : '<span') +
					' title="' + monthNames[i] + '">' + monthNamesShort[i] +
					(inRange ? '</a>' : '</span>') + '</div>';
			}
			for (i = -6; i <= 6; i++) {
				if (i === 0) {
					continue;
				}
				inRange =
					((!minDate || new Date(year + i, 12 - 1, 31).getTime() >= minDate.getTime()) &&
					(!maxDate || new Date(year + i, 1 - 1, 1).getTime() <= maxDate.getTime()));
				html += '<div>' + (inRange ? '<a href="#" class="dp' +
					new Date(year + i, month, 1).getTime() + '"' : '<span') +
					' title="' + (year + i) + '">' + (year + i) +
					(inRange ? '</a>' : '</span>') + '</div>';
			}
			html += '</div>';
			html = $(html).insertAfter(picker.find('div.datepick-nav,div.ui-datepicker-header:first'));
			html.find('a').click(function() {
					var date = $.datepick.retrieveDate(target[0], this);
					html.slideToggle(function() {
						target.datepick('showMonth', date.getFullYear(), date.getMonth() + 1);
					});
					return false;
				});
			picker.find('div.datepick-month-header,div.ui-datepicker-month-header').click(function() {
				html.slideToggle();
			}).css('cursor', 'pointer');
		},

		/** Select an entire week when clicking on a week number.
			Use it with the {@linkcode module:Datepick~defaultOptions|onShow} option.
			Use in conjunction with {@linkcode module:Datepick-ext~weekOfYearRenderer|weekOfYearRenderer} or
			{@linkcode module:Datepick-ext~themeRollerWeekOfYearRenderer|themeRollerWeekOfYearRenderer}.
			@param {jQuery} picker The completed datepicker division.
			@param {object} inst The current instance settings.
			@example onShow: $.datepick.selectWeek */
		selectWeek: function(picker, inst) {
			var target = $(this);
			picker.find('td.datepick-week span,td.ui-state-hover span').each(function() {
				$('<a href="#" class="' +
						this.className + '" title="Select the entire week">' +
						$(this).text() + '</a>').
					click(function() {
						var date = target.datepick('retrieveDate', this);
						var dates = [date];
						for (var i = 1; i < 7; i++) {
							dates.push(date = $.datepick.add($.datepick.newDate(date), 1, 'd'));
						}
						if (inst.get('rangeSelect')) {
							dates.splice(1, dates.length - 2);
						}
						target.datepick('setDate', dates).datepick('hide');
						return false;
					}).
					replaceAll(this);
			});
		},

		/** Select an entire month when clicking on the week header.
			Use it with the {@linkcode module:Datepick~defaultOptions|onShow} option.
			Use in conjunction with {@linkcode module:Datepick-ext~weekOfYearRenderer|weekOfYearRenderer} or
			{@linkcode module:Datepick-ext~themeRollerWeekOfYearRenderer|themeRollerWeekOfYearRenderer}.
			@param {jQuery} picker The completed datepicker division.
			@param {object} inst The current instance settings.
			@example onShow: $.datepick.selectMonth */
		selectMonth: function(picker, inst) {
			var target = $(this);
			picker.find('th.datepick-week span,th.ui-state-hover span').each(function() {
				$('<a href="#" title="Select the entire month">' +
						$(this).text() + '</a>').
					click(function() {
						var date = target.datepick('retrieveDate', $(this).parents('table').
							find('td:not(.datepick-week):not(.ui-state-hover) ' +
								'*:not(.datepick-other-month):not(.ui-datepicker-other-month)')[0]);
						var dates = [date];
						var dim = $.datepick.daysInMonth(date);
						for (var i = 1; i < dim; i++) {
							dates.push(date = $.datepick.add($.datepick.newDate(date), 1, 'd'));
						}
						if (inst.get('rangeSelect')) {
							dates.splice(1, dates.length - 2);
						}
						target.datepick('setDate', dates).datepick('hide');
						return false;
					}).
					replaceAll(this);
			});
		},

		/** Select a month only instead of a single day.
			Use it with the {@linkcode module:Datepick~defaultOptions|onShow} option.
			@param {jQuery} picker The completed datepicker division.
			@param {object} inst The current instance settings.
			@example onShow: $.datepick.monthOnly */
		monthOnly: function(picker, inst) { // jshint unused:false
			var target = $(this);
			$('<div style="text-align: center;"><button type="button">Select</button></div>').
				insertAfter(picker.find('.datepick-month-row:last,.ui-datepicker-row-break:last')).
				children().click(function() {
					var monthYear = picker.find('.datepick-month-year:first').val().split('/');
					target.datepick('setDate', $.datepick.newDate(
						parseInt(monthYear[1], 10), parseInt(monthYear[0], 10), 1)).
						datepick('hide');
				});
			picker.find('.datepick-month-row table,.ui-datepicker-row-break table').remove();
		}
	});

})(jQuery);