webpackJsonp([2],{

/***/ "../../../../../client-src/content/css/style.css":
/***/ (function(module, exports, __webpack_require__) {

// style-loader: Adds some css to the DOM by adding a <style> tag

// load the styles
var content = __webpack_require__("../../../../css-loader/index.js?{\"sourceMap\":false,\"importLoaders\":1}!../../../../postcss-loader/index.js?{\"ident\":\"postcss\"}!../../../../../client-src/content/css/style.css");
if(typeof content === 'string') content = [[module.i, content, '']];
// add the styles to the DOM
var update = __webpack_require__("../../../../style-loader/addStyles.js")(content, {});
if(content.locals) module.exports = content.locals;
// Hot Module Replacement
if(false) {
	// When the styles change, update the <style> tags
	if(!content.locals) {
		module.hot.accept("!!../../../node_modules/css-loader/index.js??ref--9-1!../../../node_modules/postcss-loader/index.js??postcss!./style.css", function() {
			var newContent = require("!!../../../node_modules/css-loader/index.js??ref--9-1!../../../node_modules/postcss-loader/index.js??postcss!./style.css");
			if(typeof newContent === 'string') newContent = [[module.id, newContent, '']];
			update(newContent);
		});
	}
	// When the module is disposed, remove the <style> tags
	module.hot.dispose(function() { update(); });
}

/***/ }),

/***/ "../../../../../client-src/content/img/standard-bank-logo-mark.svg":
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__.p + "standard-bank-logo-mark.55bea7c1cb84f21888e2.svg";

/***/ }),

/***/ "../../../../css-loader/index.js?{\"sourceMap\":false,\"importLoaders\":1}!../../../../postcss-loader/index.js?{\"ident\":\"postcss\"}!../../../../../client-src/content/css/style.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".header{background:#18326f}.header .logo{background-image:url(" + __webpack_require__("../../../../../client-src/content/img/standard-bank-logo-mark.svg") + ");height:50px;width:240px;background-repeat:no-repeat;margin:10px 10px 10px 0}.search-and-results-container{padding-top:20px}.search-and-results-container .input-group{width:100%;padding-bottom:10px}.search-and-results-container .input-group #srch-term{border-radius:5px}.search-and-results-container .section .small-table-title{width:200px;font-weight:500;font-size:1.25em}.search-and-results-container .section .small-table-title .concessionID-section{padding-left:7px;margin:-7px 0}.table-hover-highlight:hover .section-header{border:1px solid #18326f;border-bottom:none;background:rgba(24,50,111,.2)}.table-hover-highlight:hover .section-header .single-print{visibility:visible}.btn-default-search{background-color:#fff;border:1px solid #bababa;border-left:none}.centerText{text-align:center}body{background-color:#ededed}.section{border:1px solid #006892;margin-bottom:15px}.section .section-body{background-color:#fff}.section .section-header{background:#fff;width:20%;border:1px solid #e3e9e9;border-bottom:none}.section .section-body{padding:10px;box-shadow:0 2px 2px #adadad}.section .section-footer{padding:5px;text-align:right}.summary-banner{color:#fff;background-color:#006892;width:100%}.summary-banner .summary-banner-title{background-color:#3386a8;padding:10px 0;margin-bottom:10px;text-align:center;font-size:1.5em}.summary-banner .summary-banner-title .banner-main-title{text-justify:center}.summary-banner .summary-items-sub{margin-bottom:10px;padding-left:0}.summary-item-value{font-weight:700;font-size:1.6em;padding:0}input{border:1px solid #adadad}.darkGrayText{color:#3f3f3f}.lightTitle{color:#ededed}#compress-box{position:fixed}#compress-box:checked>div>.hiding-banner{visibility:hidden;height:100px}.compress{float:right;font-size:1.25em;cursor:pointer}.hidden-element{display:block}.concession-footer{margin-top:10px}.concession-footer ul{margin:0}.search-container{padding-top:20px;margin-left:10px}.search-container .form-horizontal{background-color:#fff;padding-top:20px;color:gray}.searchEmptyState{text-align:center;padding:100px;color:#3f3f3f;vertical-align:middle}.searchEmptyState i{font-size:1.5em}.searchEmptyState div{display:inline;margin:0;padding:5px 0}.pagination{float:right;margin:10px 0}.btn-no{background-color:#960011;box-shadow:1px 2px 1px #3f3f3f;color:#fff;width:75px}.btn-no:hover{background:rgba(150,0,17,.75)}.btn-yes{background-color:#3f6e00;box-shadow:1px 2px 1px #3f3f3f;color:#fff;width:75px}.btn-yes:hover{background:rgba(63,110,0,.75)}.btn-print{background-color:#18326f;color:#fff;box-shadow:1px 2px 1px #18326f;padding:5px 10px}.btn-print:hover{color:#fff}.btn-print:focus{color:#fff;background:#18326f}.concessionBtn{background:#18326f;box-shadow:1px 2px 1px #18326f;float:right}.concessionBtn:hover{color:#fff;background:rgba(24,50,111,.75)}.btn-default-search,.concessionBtn:focus{color:#fff;background:#18326f}.btn-default-search:hover{background:rgba(24,50,111,.75);color:#fff}.btn-default-search:focus,.btn-submit{color:#fff;background:#18326f}.btn-submit{box-shadow:1px 2px 1px #18326f;padding:5px 30px;min-width:75px}.btn-submit:hover{background:rgba(24,50,111,.75);color:#fff}.btn-submit:focus{color:#fff;background:#18326f}.btn-cancel{min-width:75px;padding:5px 30px;background:#ededed;box-shadow:0 2px 5px}.btn-cancel:hover{background:#fafafa}.btn-default{background:#18326f;color:#fff;box-shadow:1px 2px 1px #18326f;padding:5px 0;min-width:145px;border:none}.btn-default:hover{background-color:rgba(24,50,111,.75);color:#fff}.btn-default:active,.btn-default:active:hover,.btn-default:focus{background:rgba(24,50,111,.75);color:#fff}.nav-pills-container{background-color:#fff;box-shadow:1px 2px 1px #a5a5a5}.nav-pills-container .nav-pills li{border-bottom:2px solid #fff}.nav-pills-container .nav-pills li a{color:#757575;font-size:1.2em;margin-left:10px;background-color:#fff}.nav-pills-container .nav-pills li:hover a{text-decoration:none;color:#adadad}.nav-pills-container .nav-pills li:active{color:#006892;background:none;background-color:#fff}.nav-pills-container .nav-pills .logout-li{float:right}.nav-pills-container .nav-pills .selected-nav-item a{color:#2f91ff}.table-container table{border:none;margin-bottom:0;background:#f9f9f9}.table-container table tr:hover td{background:#f9f9f9}.table-container th{background-color:#006892;color:#fff;border-width:0}.table-container thead>tr>th{border-width:0}.table-container td{font-size:.9em;vertical-align:middle}.table-container .status-red{background-color:#c03343!important;color:#fff}.table-container .status-red:hover td{background:#c03343}.table-container .status-green{background-color:#739c3c;color:#fff}.table-container .status-green:hover td{background:#739c3c}.table-container .status-yellow{background-color:#efad1e!important;color:#fff}.table-container .status-yellow:hover td{background:#efad1e}.product-table-container .table{border:none}.product-table-container .table td:hover,.product-table-container .table tr:hover{background:#f9f9f9}.product-table-container th{background-color:#adadad;color:#fff;border-width:0}.product-table-container thead>tr>th{border-width:0}.form-concessions-table{margin-bottom:25px}.form-concessions-table .form-concessions-table-content tr input,.form-concessions-table .form-concessions-table-content tr select{width:100%;height:28px;padding-left:3px;border:1px solid #adadad}.form-concessions-table .form-concessions-table-content tr td{padding-right:10px;height:25px;width:100%}.form-concessions-table .form-concessions-table-content tr .length-long{width:15%}.form-concessions-table .form-concessions-table-content tr .length-medium{width:8%}.form-concessions-table .form-concessions-table-content tr .length-short{width:7%}.form-concessions-table .form-concessions-table-content tr .delete{width:2%}.form-concessions-table .delete{font-size:25px;display:inline}.form-concessions-table .delete div{width:100%}#manageConditions .modal-dialog{margin:10%;width:80%;background:#ededed}#manageConditions .modal-dialog .modal-header{background:#18326f;color:#fff}#manageConditions .modal-dialog .modal-header h4{margin:0}#manageConditions .modal-dialog .modal-title{padding-bottom:15px}#manageConditions .modal-dialog .modal-body .form-concessions-table-content tr td{padding-right:10px;height:25px}#manageConditions .modal-dialog .modal-body .form-concessions-table-content tr .length-long{width:20%}#manageConditions .modal-dialog .modal-body .form-concessions-table-content tr .length-medium{width:9.5%}#manageConditions .modal-dialog .modal-body .form-concessions-table-content tr .delete{width:2%}.delete i{cursor:pointer;color:#3f3f3f}.totalsWidget{float:left;margin-top:10px}.totalsWidget:hover{cursor:pointer}.totalsWidget:hover .cornered{border-bottom:20px solid rgba(0,104,146,.75);color:#fafafa}.totalsWidget:hover .main{background:rgba(0,104,146,.75)}.totalsWidget:hover .main p{color:#fff}.totalsWidget .activeWidget .cornered{border-bottom:20px solid #006892;color:#fafafa}.totalsWidget .activeWidget .main{background:#006892}.totalsWidget .activeWidget .main p{color:#fff}.outer{font-size:x-small}.outer .cornered{width:150px;height:0;color:#2f91ff;font-size:15px;text-align:left;padding-left:10px;margin-top:5px;border-bottom:20px solid #fff;border-right:20px solid transparent;border-top-left-radius:3px}.outer .cornered p{padding-top:5px}.outer .main{width:150px;height:50px;text-align:left;background-color:#fff;padding:0 8px;box-shadow:1px 2px 1px #a5a5a5;border-bottom-left-radius:3px;border-bottom-right-radius:3px}.outer .main p{font-size:2.9em;padding:7px 0 0 2px;color:#494949;font-weight:700}.customerName,.date{margin:0;padding:0}.accInfo,.mapInfo{margin:0;padding:0;color:#757575;font-size:1.1em}.customerInfo{margin:0;padding:0;font-size:1em}.filter-group{padding-bottom:30px}.filter-group .control-label{width:3%;color:#3f3f3f;padding:7px}#filter{vertical-align:middle}.section .section-header{width:100%}.single-print{float:right;visibility:hidden;cursor:pointer}.single-print i{font-size:30px}.single-print i:hover{color:#adadad}.no-padding-left{padding-left:0}.vertical-align-center{margin:8px}.concession-name{color:#006892;font-size:1.15em;font-weight:700}.search-and-results-container .section{padding:0}.search-and-results-container .section .section-header{padding:10px 0}.pricing-banner{background-color:#006892!important;padding:20px 0}.pricing-banner .pricing-user-image{color:#fff;font-size:50px}.pricing-banner p{margin:10px 0 0}.padding-div{padding:10px}.pricing-form{color:#fff!important}.pricing-group-container{margin-top:80px;background-color:#fff}.pricing-group-container .pricing-group-info{display:-ms-grid;display:grid;-webkit-box-pack:center;-ms-flex-pack:center;justify-content:center}.pricing-group-container .pricing-group-info h3{margin-bottom:-10px}.pricing-group-container .pricing-group-info .secondaryText{color:#757575;text-align:center;font-size:1.25em}.pricing-group-container .pricing-icon{width:100px;border-radius:50px;text-align:center;font-size:50px;box-shadow:0 3px 5px #adadad;color:#006892;margin:0 auto -50px;position:relative;top:-50px;background:#fff}.pricing-group-container .pricing-icon .building-icon{padding:25px}.pricing-group-container .pricing-icon .building-icon i{display:block}.pricing-group-container .pricing-group-container-items{display:-webkit-box;display:-ms-flexbox;display:flex;width:100%;padding:20px;background-color:inherit}.pricing-group-container .pricing-group-container-items .item{margin:2px;background-color:#9c9c9c;width:12.5%;height:100px;color:#fff;padding:10px;cursor:pointer}.pricing-group-container .pricing-group-container-items .item .container-item-icon{margin-top:10px;background-color:#1d71d3;width:50px;height:50px;border-radius:25px;border:1px solid #fff;padding-top:12.5px;float:right}.pricing-group-container .pricing-group-container-items .item .container-item-icon i{display:block;text-align:center;font-size:25px}.pricing-group-container .pricing-group-container-items .selected-item{background-color:#2f91ff}.lending-banner-title{background-color:#3386a8;padding:10px 0;text-align:center;color:#fff;display:-webkit-box;display:-ms-flexbox;display:flex;font-size:1.5em}.lending-banner-title .banner-main-title{text-justify:center}.lending-banner-title .back-button-text{font-size:.8em;cursor:pointer}.lending-banner-title .back-button-text:hover{text-decoration:underline}.lending-view-banner{background:#006892}.lending-view-banner .lending-banner{background:#006892;color:#fff;padding-top:15px;padding-bottom:15px}.lending-view-banner .lending-banner .subHeading{font-size:1.1em}.lending-view-banner .lending-banner .score{font-size:1.9em}.lending-headings{color:#757575}.consessionID-section{width:20%;padding:2px 2px 2px 10px;font-size:1.1em;margin-top:12px}.product-section{background:#fff;padding-left:7px;font-weight:700}.product-section .product-name{color:#006892;font-size:1.2em;padding-top:5px;margin:0}.product-section .product-code{float:right;padding-right:60px;font-size:1em;color:#757575}.product-section .product-account{float:left;font-size:1em;color:#757575;padding-bottom:7px}.rightAlign{text-align:right}#lending-search-container{padding-top:0}.lightText{color:#fafafa}.table-title{margin:20px 0;font-weight:700;color:#757575}.section{border:none}.section,.section .section-body{padding:0}.concessions .section{background:none}.concessions .section .section-header{background:#fff;width:20%;padding:5px 10px}.concessions .section .concessionID-section{color:#000}.concessions .section table,.concessions .section table tr{background:#fff}.concessions-table-head{background-color:#006892;color:#fff}.secondaryText{color:#757575;padding-left:0}.secondaryText .normalText{display:inline-block;color:#000}.cash-table{border-collapse:collapse;background-color:#fff}.cash-table td,.cash-table th{padding:0 5px;border:1px solid #bababa}.concessions-top-title{padding-top:20px;margin-bottom:20px}.concessions-top-title button,.concessions-top-title h3{display:inline}.concessions-top-title button{float:right}.lending-bottom-buttons{padding-bottom:20px;position:relative;bottom:0}.lending-bottom-buttons .float-right{float:right}.lending-bottom-buttons .float-right button{margin-left:15px}.concession-information-form label{padding-top:10px}.concession-information-form .large-input{height:88px}label,th{color:#787878;font-weight:400}", ""]);

// exports


/***/ }),

/***/ "../../../../css-loader/lib/css-base.js":
/***/ (function(module, exports) {

/*
	MIT License http://www.opensource.org/licenses/mit-license.php
	Author Tobias Koppers @sokra
*/
// css base code, injected by the css-loader
module.exports = function(useSourceMap) {
	var list = [];

	// return the list of modules as css string
	list.toString = function toString() {
		return this.map(function (item) {
			var content = cssWithMappingToString(item, useSourceMap);
			if(item[2]) {
				return "@media " + item[2] + "{" + content + "}";
			} else {
				return content;
			}
		}).join("");
	};

	// import a list of modules into the list
	list.i = function(modules, mediaQuery) {
		if(typeof modules === "string")
			modules = [[null, modules, ""]];
		var alreadyImportedModules = {};
		for(var i = 0; i < this.length; i++) {
			var id = this[i][0];
			if(typeof id === "number")
				alreadyImportedModules[id] = true;
		}
		for(i = 0; i < modules.length; i++) {
			var item = modules[i];
			// skip already imported module
			// this implementation is not 100% perfect for weird media query combinations
			//  when a module is imported multiple times with different media queries.
			//  I hope this will never occur (Hey this way we have smaller bundles)
			if(typeof item[0] !== "number" || !alreadyImportedModules[item[0]]) {
				if(mediaQuery && !item[2]) {
					item[2] = mediaQuery;
				} else if(mediaQuery) {
					item[2] = "(" + item[2] + ") and (" + mediaQuery + ")";
				}
				list.push(item);
			}
		}
	};
	return list;
};

function cssWithMappingToString(item, useSourceMap) {
	var content = item[1] || '';
	var cssMapping = item[3];
	if (!cssMapping) {
		return content;
	}

	if (useSourceMap && typeof btoa === 'function') {
		var sourceMapping = toComment(cssMapping);
		var sourceURLs = cssMapping.sources.map(function (source) {
			return '/*# sourceURL=' + cssMapping.sourceRoot + source + ' */'
		});

		return [content].concat(sourceURLs).concat([sourceMapping]).join('\n');
	}

	return [content].join('\n');
}

// Adapted from convert-source-map (MIT)
function toComment(sourceMap) {
	// eslint-disable-next-line no-undef
	var base64 = btoa(unescape(encodeURIComponent(JSON.stringify(sourceMap))));
	var data = 'sourceMappingURL=data:application/json;charset=utf-8;base64,' + base64;

	return '/*# ' + data + ' */';
}


/***/ }),

/***/ "../../../../style-loader/addStyles.js":
/***/ (function(module, exports) {

/*
	MIT License http://www.opensource.org/licenses/mit-license.php
	Author Tobias Koppers @sokra
*/
var stylesInDom = {},
	memoize = function(fn) {
		var memo;
		return function () {
			if (typeof memo === "undefined") memo = fn.apply(this, arguments);
			return memo;
		};
	},
	isOldIE = memoize(function() {
		return /msie [6-9]\b/.test(self.navigator.userAgent.toLowerCase());
	}),
	getHeadElement = memoize(function () {
		return document.head || document.getElementsByTagName("head")[0];
	}),
	singletonElement = null,
	singletonCounter = 0,
	styleElementsInsertedAtTop = [];

module.exports = function(list, options) {
	if(typeof DEBUG !== "undefined" && DEBUG) {
		if(typeof document !== "object") throw new Error("The style-loader cannot be used in a non-browser environment");
	}

	options = options || {};
	// Force single-tag solution on IE6-9, which has a hard limit on the # of <style>
	// tags it will allow on a page
	if (typeof options.singleton === "undefined") options.singleton = isOldIE();

	// By default, add <style> tags to the bottom of <head>.
	if (typeof options.insertAt === "undefined") options.insertAt = "bottom";

	var styles = listToStyles(list);
	addStylesToDom(styles, options);

	return function update(newList) {
		var mayRemove = [];
		for(var i = 0; i < styles.length; i++) {
			var item = styles[i];
			var domStyle = stylesInDom[item.id];
			domStyle.refs--;
			mayRemove.push(domStyle);
		}
		if(newList) {
			var newStyles = listToStyles(newList);
			addStylesToDom(newStyles, options);
		}
		for(var i = 0; i < mayRemove.length; i++) {
			var domStyle = mayRemove[i];
			if(domStyle.refs === 0) {
				for(var j = 0; j < domStyle.parts.length; j++)
					domStyle.parts[j]();
				delete stylesInDom[domStyle.id];
			}
		}
	};
}

function addStylesToDom(styles, options) {
	for(var i = 0; i < styles.length; i++) {
		var item = styles[i];
		var domStyle = stylesInDom[item.id];
		if(domStyle) {
			domStyle.refs++;
			for(var j = 0; j < domStyle.parts.length; j++) {
				domStyle.parts[j](item.parts[j]);
			}
			for(; j < item.parts.length; j++) {
				domStyle.parts.push(addStyle(item.parts[j], options));
			}
		} else {
			var parts = [];
			for(var j = 0; j < item.parts.length; j++) {
				parts.push(addStyle(item.parts[j], options));
			}
			stylesInDom[item.id] = {id: item.id, refs: 1, parts: parts};
		}
	}
}

function listToStyles(list) {
	var styles = [];
	var newStyles = {};
	for(var i = 0; i < list.length; i++) {
		var item = list[i];
		var id = item[0];
		var css = item[1];
		var media = item[2];
		var sourceMap = item[3];
		var part = {css: css, media: media, sourceMap: sourceMap};
		if(!newStyles[id])
			styles.push(newStyles[id] = {id: id, parts: [part]});
		else
			newStyles[id].parts.push(part);
	}
	return styles;
}

function insertStyleElement(options, styleElement) {
	var head = getHeadElement();
	var lastStyleElementInsertedAtTop = styleElementsInsertedAtTop[styleElementsInsertedAtTop.length - 1];
	if (options.insertAt === "top") {
		if(!lastStyleElementInsertedAtTop) {
			head.insertBefore(styleElement, head.firstChild);
		} else if(lastStyleElementInsertedAtTop.nextSibling) {
			head.insertBefore(styleElement, lastStyleElementInsertedAtTop.nextSibling);
		} else {
			head.appendChild(styleElement);
		}
		styleElementsInsertedAtTop.push(styleElement);
	} else if (options.insertAt === "bottom") {
		head.appendChild(styleElement);
	} else {
		throw new Error("Invalid value for parameter 'insertAt'. Must be 'top' or 'bottom'.");
	}
}

function removeStyleElement(styleElement) {
	styleElement.parentNode.removeChild(styleElement);
	var idx = styleElementsInsertedAtTop.indexOf(styleElement);
	if(idx >= 0) {
		styleElementsInsertedAtTop.splice(idx, 1);
	}
}

function createStyleElement(options) {
	var styleElement = document.createElement("style");
	styleElement.type = "text/css";
	insertStyleElement(options, styleElement);
	return styleElement;
}

function createLinkElement(options) {
	var linkElement = document.createElement("link");
	linkElement.rel = "stylesheet";
	insertStyleElement(options, linkElement);
	return linkElement;
}

function addStyle(obj, options) {
	var styleElement, update, remove;

	if (options.singleton) {
		var styleIndex = singletonCounter++;
		styleElement = singletonElement || (singletonElement = createStyleElement(options));
		update = applyToSingletonTag.bind(null, styleElement, styleIndex, false);
		remove = applyToSingletonTag.bind(null, styleElement, styleIndex, true);
	} else if(obj.sourceMap &&
		typeof URL === "function" &&
		typeof URL.createObjectURL === "function" &&
		typeof URL.revokeObjectURL === "function" &&
		typeof Blob === "function" &&
		typeof btoa === "function") {
		styleElement = createLinkElement(options);
		update = updateLink.bind(null, styleElement);
		remove = function() {
			removeStyleElement(styleElement);
			if(styleElement.href)
				URL.revokeObjectURL(styleElement.href);
		};
	} else {
		styleElement = createStyleElement(options);
		update = applyToTag.bind(null, styleElement);
		remove = function() {
			removeStyleElement(styleElement);
		};
	}

	update(obj);

	return function updateStyle(newObj) {
		if(newObj) {
			if(newObj.css === obj.css && newObj.media === obj.media && newObj.sourceMap === obj.sourceMap)
				return;
			update(obj = newObj);
		} else {
			remove();
		}
	};
}

var replaceText = (function () {
	var textStore = [];

	return function (index, replacement) {
		textStore[index] = replacement;
		return textStore.filter(Boolean).join('\n');
	};
})();

function applyToSingletonTag(styleElement, index, remove, obj) {
	var css = remove ? "" : obj.css;

	if (styleElement.styleSheet) {
		styleElement.styleSheet.cssText = replaceText(index, css);
	} else {
		var cssNode = document.createTextNode(css);
		var childNodes = styleElement.childNodes;
		if (childNodes[index]) styleElement.removeChild(childNodes[index]);
		if (childNodes.length) {
			styleElement.insertBefore(cssNode, childNodes[index]);
		} else {
			styleElement.appendChild(cssNode);
		}
	}
}

function applyToTag(styleElement, obj) {
	var css = obj.css;
	var media = obj.media;

	if(media) {
		styleElement.setAttribute("media", media)
	}

	if(styleElement.styleSheet) {
		styleElement.styleSheet.cssText = css;
	} else {
		while(styleElement.firstChild) {
			styleElement.removeChild(styleElement.firstChild);
		}
		styleElement.appendChild(document.createTextNode(css));
	}
}

function updateLink(linkElement, obj) {
	var css = obj.css;
	var sourceMap = obj.sourceMap;

	if(sourceMap) {
		// http://stackoverflow.com/a/26603875
		css += "\n/*# sourceMappingURL=data:application/json;base64," + btoa(unescape(encodeURIComponent(JSON.stringify(sourceMap)))) + " */";
	}

	var blob = new Blob([css], { type: "text/css" });

	var oldSrc = linkElement.href;

	linkElement.href = URL.createObjectURL(blob);

	if(oldSrc)
		URL.revokeObjectURL(oldSrc);
}


/***/ }),

/***/ 0:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__("../../../../../client-src/content/css/style.css");


/***/ })

},[0]);
//# sourceMappingURL=styles.bundle.js.map