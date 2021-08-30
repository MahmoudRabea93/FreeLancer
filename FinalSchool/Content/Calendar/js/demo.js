/* Common JavaScript for jQuery demonstration pages. */
$(function () {
	$('head').prepend('<link href="kw-name.rss.xml" type="application/rss+xml" rel="alternate" title="RSS Feed">');
	$('body').attr('style', ''); // Remove GoDaddy styling
	// Add header links
	var current = location.href.replace(/^.*[\\\/]([^\.]+)\.[^\.]+$/, '$1').toLowerCase();
	var git = {backgroundpos: 'backgroundpos', bookmark: 'bookmark', calculator: 'calculator',
		calendars: 'calendars', calendarspicker: 'calendars', countdown: 'countdown',
		dateentry: 'dateentry', datetimeentry: 'datetimeentry', datepick: 'datepick',
		flightboard: 'flightboard', gchart: 'gchart', gsblogbar: 'gsblogbar',
		gsbookbar: 'gsbookbar', gsnewsbar: 'gsnewsbar', gsvideobar: 'gsvideobar',
		icalendar: 'icalendar', imagecube: 'imagecube', keypad: 'keypad',
		labeleffect: 'labeleffect', linkedsliders: 'linkedsliders', localisation: 'localisation',
		maxlength: 'maxlength', more: 'more', realperson: 'realperson', relationships: 'relationships',
		signature: 'signature', svg: 'svg', themes: 'themes', timeentry: 'timeentry'};
	$('<div id="header-links">' +
		(current == 'index' ? '' : '<a id="home" href="index.html">' +
		'<img src="img/homePage.png" alt="Home page" title="Home page"></a>&nbsp;') +
		'<a href="#" id="donate"><img src="img/donate.png" alt="Donate" title="Donate"></a>&nbsp;' +
		'<div id="donations"><h3>Donations</h3><div>If you would like to donate something for my efforts, please visit my ' +
		'<a href="http://www.amazon.com/wishlist/2OEDAIL14M64W/ref=cm_wl_rlist_go" target="_blank">Amazon Wishlist</a> ' +
		'or donate directly through <form action="https://www.paypal.com/cgi-bin/webscr" method="post" target="_blank">' +
		'<input type="hidden" name="cmd" value="_s-xclick">' +
		'<input type="hidden" name="encrypted" value="-----BEGIN PKCS7-----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-----END PKCS7-----' +
		'"><input type="image" src="http://keith-wood.name/img/paypal.png" border="0" name="submit" alt="PayPal - The safer, easier way to pay online.">' +
		'<img alt="" border="0" src="https://www.paypalobjects.com/en_AU/i/scr/pixel.gif" width="1" height="1">' +
		'</form>.</div>' +
		'<p>Thank you to those that have already donated - I\'m enjoying the music and dancing!</p></div>' +
		'<a href="#" id="crosslink"><img src="img/plugins.png" alt="Other plugins" title="Other plugins"></a>&nbsp;' +
		'<div id="crosslinks"><ul>' +
		(current == 'backgroundpos' ? '' : '<li><a href="backgroundPos.html">Background Position</a></li>') +
		(current == 'bookmark'      ? '' : '<li><a href="bookmark.html">Bookmark</a></li>') +
		(current == 'calculator'    ? '' : '<li><a href="calculator.html">Calculator</a></li>') +
		(current == 'calendarspicker' ||
		 current == 'calendars'     ? '' : '<li><a href="calendars.html">Calendars</a></li>') +
		(current == 'countdown'     ? '' : '<li><a href="countdown.html">Countdown</a></li>') +
		(current == 'dateentry'     ? '' : '<li><a href="dateEntry.html">Date Entry</a></li>') +
		(current == 'datetimeentry' ? '' : '<li><a href="datetimeEntry.html">Date/Time Entry</a></li>') +
		(current == 'datepick'      ? '' : '<li><a href="datepick.html">Datepicker</a></li>') +
		(current == 'flightboard'   ? '' : '<li><a href="flightBoard.html">Flight Board</a></li>') +
		(current == 'gchart'        ? '' : '<li><a href="gChart.html">Google Chart</a></li>') +
		(current == 'gsblogbar'     ? '' : '<li><a href="gsblogbar.html">Google Search Blogbar</a></li>') +
		(current == 'gsbookbar'     ? '' : '<li><a href="gsbookbar.html">Google Search Bookbar</a></li>') +
		(current == 'gsnewsbar'     ? '' : '<li><a href="gsnewsbar.html">Google Search Newsbar</a></li>') +
		(current == 'gsvideobar'    ? '' : '<li><a href="gsvideobar.html">Google Search Videobar</a></li>') +
		'</ul><ul>' +
		(current == 'icalendar'     ? '' : '<li><a href="icalendar.html">iCalendar</a></li>') +
		(current == 'imagecube'     ? '' : '<li><a href="imageCube.html">Image Cube</a></li>') +
		(current == 'keypad'        ? '' : '<li><a href="keypad.html">Keypad</a></li>') +
		(current == 'labeleffect'   ? '' : '<li><a href="labelEffect.html">Label Effect</a></li>') +
		(current == 'linkedsliders' ? '' : '<li><a href="linkedSliders.html">Linked Sliders</a></li>') +
		(current == 'localisation'  ? '' : '<li><a href="localisation.html">Localisation</a></li>') +
		(current == 'maxlength'     ? '' : '<li><a href="maxlength.html">Max Length</a></li>') +
		(current == 'more'          ? '' : '<li><a href="more.html">More</a></li>') +
		(current == 'realperson'    ? '' : '<li><a href="realPerson.html">Real Person</a></li>') +
		(current == 'relationships' ? '' : '<li><a href="relationships.html">Relationships</a></li>') +
		(current == 'signature'     ? '' : '<li><a href="signature.html">Signature</a></li>') +
		(current == 'svg'           ? '' : '<li><a href="svg.html">SVG Integration</a></li>') +
		(current == 'themes'        ? '' : '<li><a href="themes.html">Themes</a></li>') +
		(current == 'timeentry'     ? '' : '<li><a href="timeEntry.html">Time Entry</a></li>') +
		'</ul></div>' +
		'<a href="kw-name.rss.xml"><img src="img/rss.png" alt="RSS Feed" title="RSS Feed"></a>' +
		(git[current] ? '&nbsp;<a href="https://github.com/kbwood/' + git[current] + '">' +
		'<img src="img/github.png" alt="github repository" title="github repository"></a>' : '') +
		'</div>').insertBefore('h1');
	var donate = $('#donate');
	var donations = $('#donations');
	donate.click(function() {
		var dOffset = donate.offset();
		donations.css({left: dOffset.left + donate.outerWidth() - donations.outerWidth(),
			top: dOffset.top + donate.outerHeight()});
		donations.slideToggle();
	});
	var crosslink = $('#crosslink');
	var crosslinks = $('#crosslinks');
	crosslink.click(function() {
		var cOffset = crosslink.offset();
		crosslinks.css({left: cOffset.left + crosslink.outerWidth() - crosslinks.outerWidth(),
			top: cOffset.top + crosslink.outerHeight()});
		crosslinks.slideToggle();
	});
	$(document).mousedown(function(event) {
		var target = $(event.target);
		if (target.closest('#donate,#donations').length === 0) {
			donations.slideUp();
		}
		if (target.closest('#crosslink,#crosslinks').length === 0) {
			crosslinks.slideUp();
		}
	}).keydown(function(event) {
		if (event.keyCode == 27) { // Escape
			donations.slideUp();
			crosslinks.slideUp();
		}
	});
	$('#download').append(' <img src="img/download.png" alt="" style="">');
	if ($.fn.bookmark) {
		$('#bookmark').bookmark({compact: true, popup: true, icons: 'img/bookmarks.png',
			popupText: '<button>Bookmark this <img src="img/bookmarker.png" alt=""></button>',
			sites: $.bookmark.commonSites});
	}
	// Ratings
	showRating(current, '#rating', ': ');
	// Stripe tables
	$('table').each(function() {
		$('tr:odd', this).addclass('ui-state-highlight');
	});
	// Initialise tabs
	if ($.fn.tabs) {
		$('#tabs').tabs($.fn.tabs.tabProps);
	}
	// Show details on demand
	var l10ns = $('#l10nPkgs').nextAll('ul').hide();
	$('<button type="button">Show all</button>').insertBefore(l10ns).
		click(function() {
			$(this).hide().next().show();
		});
	$('#history tbody ul:not(:first)').hide();
	$('<button type="button">Show details</button>').
		prependTo($('td:last', '#history tbody tr:eq(1)')).
		click(function() {
			$(this).hide();
			$('#history tbody ul').show();
		});
	// Execute example script tags
	$('code.jsdemo').each(function () {
		$(this).parent().addclass('showCode').end().
			toggleclass('jsdemo js').hide().
			wrap('<span class="ui-state-active"></span>').
			before('<a href="#">Show code</a>').
			text('\n' + $(this).text());
		eval($(this).text().replace(/&lt;/g, '<').replace(/&gt;/g, '>'));
	});
	$('code.css').each(function () {
		$(this).parent().addclass('showCode').end().
			hide().wrap('<span class="ui-state-active"></span>').
			before('<a href="#">Show CSS</a><br>');
	});
	$('code.htmldemo').each(function () {
		$(this).parent().addclass('showCode').end().
			toggleclass('htmldemo html').hide().
			wrap('<span class="ui-state-active"></span>').
			before('<a href="#">Show HTML</a><br>');
	});
	$('pre.showCode a').click(function() {
		var hidden = $(this).text().match(/Show.*/);
		$(this).text($(this).text().replace(hidden ? /Show/ : /Hide/, hidden ? 'Hide' : 'Show')).
			parent().css({width: hidden ? 'auto' : '6em' , height: hidden ? 'auto' : '1.1em'}).find('code').toggle();
		return false;
	});
	// Code highlighting
	if ($.fn.chili) {
		$('code').chili({recipeFolder: 'js/'});
	}
	// Announcements
	var announced = /.*announced=Y.*/.test(document.cookie);
	if (!announced && new Date() < new Date(2015, 7-1, 13)) {
		$('<div title="Special Offer">My book, ' +
			'<a href="http://www.manning.com/wood/" target="_blank">Extending jQuery</a>, ' +
			'is Manning\'s Deal of the Day on July 12. Use code <strong>dotd071215au</strong> ' +
			'when purchasing for half off the normal price.</div>').dialog({dialogclass: 'announce'});
	}
	document.cookie = 'announced=Y';
});

function showRating(name, id, prefix) {
	return;
	var rating = {backgroundpos: [0.0, 0], bookmark: [4.0, 9],
		calculator: [4.5, 8], calendars: [5.0, 5], calendarspicker: [5.0, 5],
		countdown: [4.5, 51], dateentry: [4.5, 14],
		datepick: [5.0, 51], datetimeentry: [4.0, 1],
		flightboard: [4.5, 3], gchart: [4.5, 23],
		gsblogbar: [0.0, 0], gsbookbar: [0.0, 0],
		gsnewsbar: [0.0, 0], gsvideobar: [3.5, 2],
		icalendar: [4.5, 4], imagecube: [4.0, 8],
		keypad: [4.5, 10], labeleffect: [0.0, 0],
		linkedsliders: [0.0, 0], localisation: [3.0, 3],
		realperson: [4.0, 4], relationships: [4.0, 1],
		signature: [0.0, 0], svg: [4.5, 61],
		themes: [4.0, 2], timeentry: [4.0, 42]}[name];
	var html = '';
	if (rating) {
		html = (prefix || '') + '<span>';
		for (var i = 0; i < 5; i++) {
			html += '<img src="img/star' + (i + 0.5 == rating[0] ? '.5' :
				(i < rating[0] ? '1' : '0')) + '.gif">';
		}
		html += '</span> (' + rating[1] + ' vote' + (rating[1] != 1 ? 's' : '') + ')';
	}
	$(id).addclass('rating').html(html);
}

function jumpTo(tab, id) {
	var selected = $('a[href="#' + tab + '"]').parent().index();
	$('#tabs').tabs('option', {active: selected, selected: selected});
	setTimeout(function() { scrollTo(0, $('#' + id).offset().top); }, 100);
	return false;
}
