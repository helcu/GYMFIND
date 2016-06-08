$(document).ready(function(){  
  
  //------------------------------------//
  //Navbar//
  //------------------------------------//
    	var menu = $('.navbar');
    	$(window).bind('scroll', function(e){
    		if($(window).scrollTop() > 140){
    			if(!menu.hasClass('open')){
    				menu.addClass('open');
    			}
    		}else{
    			if(menu.hasClass('open')){
    				menu.removeClass('open');
    			}
    		}
    	});
    
  
  //------------------------------------//
  //Scroll To//
  //------------------------------------//
  $(".scroll").click(function(event){		
  	event.preventDefault();
  	$('html,body').animate({scrollTop:$(this.hash).offset().top}, 800);
  	
  });
  
  
  //------------------------------------//
  //Wow Animation//
  //------------------------------------// 
  wow = new WOW(
        {
          boxClass:     'wow',      // animated element css class (default is wow)
          animateClass: 'animated', // animation css class (default is animated)
          offset:       0,          // distance to the element when triggering the animation (default is 0)
          mobile:       false        // trigger animations on mobile devices (true is default)
        }
      );
      wow.init();


  //------------------------------------//
  //Scroll To Top//
  //------------------------------------// 
	//Check to see if the window is top if not then display button
	$(window).scroll(function(){
		if ($(this).scrollTop() > 300) {
			$('.scrollToTop').fadeIn();
		} else {
			$('.scrollToTop').fadeOut();
		}
	});
	//Click event to scroll to top
	$('.scrollToTop').click(function(){
		$('html, body').animate({scrollTop : 0},800);
		return false;
	});
	
	
	//------------------------------------//
  //Portfolio Filter//
  //------------------------------------//
	$(window).load(function(){
				var $container = $('#portfolio-container');
				
				$container.imagesLoaded(function() {
					$container.isotope({
						// options
						itemSelector : '.portfolio-item',
						layoutMode : 'fitRows',
						animationOptions: {
               duration: 750,
               easing: 'linear',
               queue: false
             }
					});
				});
			
				
				// filter items when filter link is clicked
				var $optionSets = $('#portfolio-filter .option-set'),
					$optionLinks = $optionSets.find('a');
			
					$optionLinks.click(function(){
					var $this = $(this);
					// don't proceed if already selected
					if ( $this.hasClass('selected') ) {
						return false;
					}
					var $optionSet = $this.parents('.option-set');
					$optionSet.find('.selected').removeClass('selected');
					$this.addClass('selected');
				
					// make option object dynamically, i.e. { filter: '.my-filter-class' }
					var options = {},
						key = $optionSet.attr('data-option-key'),
						value = $this.attr('data-option-value');
					// parse 'false' as false boolean
					value = value === 'false' ? false : value;
					options[ key ] = value;
					if ( key === 'layoutMode' && typeof changeLayoutMode === 'function' ) {
						// changes in layout modes need extra logic
						changeLayoutMode( $this, options )
					} else {
						// otherwise, apply new options
						$container.isotope( options );
					}
					
					return false;
				});
		});
		
		//------------------------------------//
    //Contact//
    //------------------------------------//

		$(document).ready(function(){
  
       $('#contact-form').validate({
        rules: {
          name: {
            minlength: 2,
            required: true
          },
          email: {
            required: true,
            email: true
          },
          message: {
            minlength: 2,
            required: true
          }
        },
        highlight: function(element) {
          $(element)
          .closest('.control-group').removeClass('success').addClass('error');
        },
        success: function(element) {
          element
          .text('OK').addClass('valid')
          .closest('.form-group').removeClass('error').addClass('success');
        }
       });
      });
      
      
  
  	
	
});

