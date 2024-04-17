function togglemenu()
        {
            const menuMobile = document.getElementById("menu_mobile")
            if(menuMobile.className =="menu_mobile-active")
            {
                menuMobile.className = "menu_mobile"
            }else
            {
                menuMobile.className = "menu_mobile-active"
            }
        }
        var _dialogSearch = document.querySelector(".dialog-search");
        function dialogSearchToggle()
        {
            _dialogSearch.classList.toggle("dialog-search-show");
        }

        var _dialogmore = document.querySelector(".dialog-more");
        function dialogMoreToggle()
        {
            _dialogmore.classList.toggle("dialog-more-show");
        }
       /* function dialogSearchToggle()
        {
           const  _dialogSearch = document.getElementById("dialog-search")
           if(_dialogSearch.className =="dialog-search-show")
            {
                _dialogSearch.className = "dialog-search"
            }else
            {
                _dialogSearch.className = "dialog-search-show"
            }
        }*/