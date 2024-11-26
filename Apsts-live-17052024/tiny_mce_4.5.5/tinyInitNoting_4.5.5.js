var customsize="14pt";
$(document).ready(function () 
{	
	
	tinymce.init({
		mode : "exact",
		elements: "ctl00$ContentPlaceHolder1$tbItemDetails",
		//elements: "noting",
		language :"en",
		browser_spellcheck: true,
		nonbreaking_force_tab : true,
		powerpaste_word_import: 'merge',
		powerpaste_html_import: 'merge',
		//autosave_restore_when_empty: false,
		//autosave_ask_before_unload: true,
		//autosave_interval: "2s",
		body_class: 'document_class',
		auto_focus:false,
		menu : {},
		toolbar_items_size: 'small',
		//autosave_retention: "6s",
		paste_data_images: true,
		height: 300,
      
		/*paste_retain_style_properties: "all",
			paste_word_valid_elements: "b,strong,i,em,h1,h2,u,p,ol,ul,li,a[href], span,color,font-size,font-color,font-family,mark",*/
		content_style: getCustomContentStyle(),
		plugins: [
			'lineheight powerpaste pramukhime tabfocus advlist  lists  image charmap  preview hr searchreplace wordcount  visualchars  code fullscreen code',
			'pagebreak fullscreen insertdatetime  nonbreaking save table  directionality emoticons textcolor colorpicker imagetools  toc  textpattern  spellchecker'
			],
			toolbar1: "pastetext removeformat | bold italic underline | alignleft aligncenter alignright alignjustify | styleselect | justifyleft justifycenter justifyright | lineheightselect | formatselect | fontselect | fontsizeselect",
			toolbar2: " bullist numlist outdent indent  | undo redo | searchreplace |   hr  " +
			"charmap | subscript superscript |  ltr rtl |  table | forecolor backcolor | preview  fullscreen | pramukhime | insertdatetime rs | pagebreak",
			image_advtab: true,
			table_tab_navigation: false,
			style_formats : [
				{title : 'Bold text', inline : 'b'},
				{title : 'Red text', inline : 'span', styles : {color : '#ff0000'}},
				{title : 'Red header', block : 'h1', styles : {color : '#ff0000'}},
				{title : 'Example 1', inline : 'span', classes : 'example1'},
				{title : 'Example 2', inline : 'span', classes : 'example2'},
				{title : 'Table styles'},
				{title : 'Table row 1', selector : 'tr', classes : 'tablerow1'}
				],
				table_default_attributes: {	border: 1,width:'99%',cellspacing:0,cellpadding:0},
				table_default_styles: {'border-collapse': 'collapse'},
				forced_root_block_attrs: { "style": "line-height: normal;margin: 0pt 0pt 0.0001pt;" },
				content_css  :"css/greenNote.css",
				images_upload_base_path: 'showImageUploadDialog',
				/*fullscreen_new_window : true,
	        fullscreen_settings : {
	                theme_advanced_path_location : "top"
	        },*/
				/**spellchecker_languages :"+English=en",**/
				fontsize_formats: " 11pt 12pt 13pt 14pt 16pt 18pt 20pt",

				external_plugins: {"nanospell": "nanospell/plugin.js"},
				nanospell_server: "java",

				init_instance_callback : function(editor) {
					editor.on('PastePostProcess', function(ed, e) {
						if (ed.node != 'undefined' && ed.node.childNodes.length > 0) {
							validatePastedView(ed.node.childNodes);
						}
					});
					editor.on('Change', function(ed, o) {
						var tables = editor.dom.select('table');
						for(var i=0; i< tables.length; i++) {
							if(tables[i].offsetWidth >= 600) {
								editor.dom.setStyle(tables[i], 'word-break', 'break-all');
								editor.dom.setStyle(tables[i], 'width', '99%');
								editor.dom.setAttrib(tables[i], 'width', '99%');
								editor.dom.setStyle(tables[i], 'page-break-before', 'avoid');
							}else{
								editor.dom.setStyle(tables[i], 'page-break-before', 'avoid');
							}
							//tables[i].classList.remove("eFileTable");
						}
						var images = editor.dom.select('img');
						for(var i=0; i< images.length; i++) {
							if(images[i].offsetWidth >= 600) {
								editor.dom.setStyle(images[i], 'max-width', '99%');
								editor.dom.setStyle(images[i], 'width', '99%');
								editor.dom.setAttrib(images[i], 'width', '99%');
							}
						}
					});
				},

				setup : function(ed)
				{
					ed.on('init', function()
							{
						//localStorage.clear();
						this.execCommand("fontName", false, "Times New Roman , Arial, Helvetica, sans-serif");
						this.execCommand("fontSize", false, "14pt");
							});    		 
					ed.addButton('rs', { text: '\u20B9', icon: false, onclick: function () {
						ed.insertContent('\u20B9');
					}
					});

					tinymce.on('AddEditor', function(ea) {
						ea.editor.on('ExecCommand',function(e) { 

							var cmd = e.command;
							var val = e.value;
							var node = e.target.selection.getNode(); 
							var nodeParent = node.parentNode;                       
							nodeParent$ = $(nodeParent);
							node$=$(node);
							if(nodeParent){
								if (cmd === "FontSize" || cmd === "FontName") {                     	  
									while(nodeParent.nodeName !='LI' && nodeParent.nodeName!='BODY'){
										nodeParent = nodeParent.parentNode;
									}
									nodeParent$ = $(nodeParent);								
									if(node.nodeName==='OL' || node.nodeName==='UL'){								
										if(cmd === "FontSize") {
											$(node.children).each(function (){
												$(this).css('font-size',val);
											});

										}
										if(cmd === "FontName") {
											$(node.children).each(function (){
												$(this).css('font-family',val);
											});

										}
									}

									if (nodeParent.nodeName === "LI" ) {
										nodeParent$.removeAttr('data-mce-style');                                
										if(cmd === "FontSize") {
											nodeParent$.css('font-size',val);
										}
										if(cmd === "FontName") {                                  
											nodeParent$.css('font-family',val);
										}                             
									}

								}
								if(cmd==='mceToggleFormat' && e.value==='bold'){

									while(nodeParent.nodeName !='LI' && nodeParent.nodeName!='BODY'){
										nodeParent = nodeParent.parentNode;
									}
									nodeParent$ = $(nodeParent);
									var strg=$(node).find('STRONG');
									if(node.childNodes[0].nodeName==='LI' && $(node).find('STRONG').length >1)
									{
										$(node.children).each(function (){
											$(this).css("font-weight", "bold");
										});
									}
									else if(node.childNodes[0].nodeName==='LI' && $(node).find('STRONG').length ==0 ){									
										$(node.children).each(function (){
											$(this).css("font-weight", "normal");
										});
									}
									else if($(nodeParent).find('STRONG').length ==1)							 
									{
										if(nodeParent.nodeName==='LI'){
											nodeParent$.css("font-weight", "bold"); 
										}
									}
									else if($(nodeParent).find('STRONG').length ==0)
									{
										nodeParent$.css("font-weight", "normal");

									}

								}
								if(cmd === 'mceInsertContent'){
//									validatePastedView(node$.context.childNodes);
									
									/*if (nodeParent.offsetParent && nodeParent.offsetParent.nodeName == 'TABLE') {
										$(nodeParent.offsetParent).css('max-width', '99%');
									}*/
									
									var trWidth =0;
									$(nodeParent).find('table').each(function () {
										$(this).removeAttr('align');
										$(this).find('td').each(function () {
											if(this.innerHTML == '&nbsp;'){
												this.innerText = "";
											}
										});
									});
									while (nodeParent!=null && nodeParent.nodeName == 'TR') {
										var totalCol = parseInt(nodeParent.cells.length);
										$(nodeParent).find('td').each(function () {
											if(this.offsetWidth >4)
												trWidth +=this.offsetWidth;
										});
										break;
									}
									if(trWidth> 600 || trWidth ==0){
										
										while (nodeParent!=null && nodeParent.nodeName == 'TR') {
											var totalCol = parseInt(nodeParent.cells.length);
											$(nodeParent).find('td').each(function () {
												 $(this).removeAttr('width');
												$(this).attr('style', 'width: ' + (99 / totalCol) + '%;'); //+ 'word-break:break-all; overflow-wrap: break-word; word-wrap: break-word;');
											});
											nodeParent=nodeParent.previousSibling;
										}
									}														
									
									
								}
							}
							

						});     

					});       

				}

				//This code has been added to resolve the cursor position issue in firefox.  	
	}).then(function(editors) {
		var isFirefox = typeof InstallTrigger !== 'undefined';
		if(isFirefox)
			$(tinymce.get("noting").getBody().children).each(function (){
				if(this.childNodes[0].attributes!=undefined && this.childNodes[0].attributes.id!=undefined && this.childNodes[0].attributes.id.value=='_mce_caret'){
					this.remove();
				}
			});
	});
	
});



function validatePastedView(nodes) {
	for (var i = 0; i < nodes.length; i++) {
		setPastedView(nodes[i]);
		if (nodes!=undefined && nodes[i]!=undefined && nodes[i].childNodes.length > 0) {
			validatePastedView(nodes[i].childNodes);
		}
	}
}

function setPastedView(node) {
		
	if (node.nodeName == 'TABLE') {
		node.style.margin = setPositionZiro(node.style.margin);
		node.style.maxWidth='99%';
		node.style.borderCollapse = 'collapse';
		$(node).removeAttr('align');
		if (node.style	&& (node.style.width == '' || node.style.width >= '443.2pt' || node.style.width >= '580px')) {
			
			node.style.breakword = 'break-all';
		}
	
		if(node.style.width!=undefined && $(node).attr('width')!='')
			node.style.width = $(node).attr('width');
    	    $(node).removeAttr('width');
		//	node.style.width = '';
		if(node.cellPadding){			
		$(node).removeAttr('cellPadding');
		}
	}
	else if (node.nodeName == 'TD') {
		if(node.style!=undefined && node.style.padding!=undefined && node.style.padding!='')
		node.style.padding = '0px 1px';
		$(node).removeAttr('width');
		$(node).removeAttr('nowrap');
		node.style.whiteSpace= '';
	}
	else if (node.nodeName == 'A') {
		if(node.firstChild && node.firstChild.nodeName=='IMG'){
			pattern=/blob:.*/;
			if(!pattern.test( node.firstChild.src)){
				$(node).remove();			
		}
	}
	else
		$(node).replaceWith(node.innerHTML);
	}
	else if (node.nodeName == 'UL') {
		node.style.paddingLeft = '15px';
	}
	else if (node.nodeName == 'P' || node.nodeName == 'DIV' || node.nodeName == 'SPAN' ) {
		if (node.style.margin)
			node.style.margin = setPositionZiro(node.style.margin);
		if (node.style.display)
			node.style.display = 'block';
		if(node.style.textIndent)
			node.style.textIndent=setPositionZiro(node.style.textIndent);
	}
	else if(node.nodeName=='LI' && node.textContent==''){
		  node.innerHTML='&nbsp';
	  }

	else if (node.nodeName == 'THEAD' || node.nodeName == 'TFOOT') {
		$(node).replaceWith(node.innerHTML);
	}
	else if ((node.nodeName == 'U' || node.nodeName == 'S') && node.textContent.trim()=='' ) {		
		$(node).replaceWith(node.innerHTML);
	}
	if(node.nodeName=="SPAN"){
		$(node).removeAttr("class");		
	}
	if (node.nodeName == 'IMG') {
	    pattern = /blob:.*/;
	    if (!pattern.test(node.src)) {
	        $(node).remove();
	    }
	}	
}
function setPositionZiro(nodeStyle) 
{
	return nodeStyle.replace(/-\d*\.?\d*/g, '0');
}
function getCustomContentStyle()
{
	customsize=$("input[id^=customsizeFontSize]").val();
	return " body,td,pre {font-size:14pt;} pre{ white-space:pre-wrap;white-space: -moz-pre-wrap;white-space: -pre-wrap;white-space: -o-pre-wrap;word-wrap: break-word;} .document_class { font-size:" + customsize +"; font-family:Times New Roman;width: 160mm; min-height: 297mm;  padding: 8px 16px 16px;margin: 8px auto; border-radius: 5px; background: white; overflow-wrap:break-word; word-wrap:break-word; overflow:scroll}html { background-color: lightgrey;} table {margin: 0px;} .document_class ul {margin: 0px;}"
}

