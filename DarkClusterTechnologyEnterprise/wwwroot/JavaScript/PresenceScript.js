presenceMenu = invoiceMenu
presenceBtn = invoiceId
lwBtn = invoiceBtn
menuClass = menuClass
lmBtn = customerBtn
ul1 = invoiceMenu

var customerBtn = document.getElementById("customerBtn");
var invoiceId = document.getElementById("invoiceId");
var invoiceBtn = document.getElementById("invoiceBtn");
var invoiceMenu = document.getElementById("sMenuInvoice");


invoiceId.onclick = async function ShowOrHide() {
    var menuClass = invoiceBtn.classList.contains('odd');

    if (listenerIsFree) {
        listenerIsFree = false;
        document.getElementById("presenceIcon").classList.add("rotate");
        if (menuClass == true) {
            invoiceMenu.classList.remove('sub-menu');
            invoiceMenu.classList.add('sub-menu-open');

            invoiceBtn.classList.remove('odd');
            invoiceBtn.classList.add('oddA');

            customerBtn.classList.remove('lmd');
            customerBtn.classList.add('lmdA');

            await sleep(280);
            invoiceMenu.classList.add('autoHeight');
            invoiceBtn.classList.add('autoHeight');
            customerBtn.classList.add('autoHeight');
        }
        else {
            debugger;
            CheckWhatsOpen();
            if (subMenuOpen) {
                await sleep(450);
            }

            if (document.getElementById("presenceIcon").classList.contains("rotate-static"))
                document.getElementById("presenceIcon").classList.remove("rotate-static");
            document.getElementById("presenceIcon").classList.remove("rotate");
        invoiceMenu.classList.remove('autoHeight');
        invoiceBtn.classList.remove('autoHeight');
        invoiceBtn.classList.remove('autoHeight');
            await sleep(10);

        invoiceMenu.classList.remove('sub-menu-open');
        invoiceMenu.classList.add('sub-menu');

        invoiceBtn.classList.remove('oddA');
        invoiceBtn.classList.add('odd');

        customerBtn.classList.remove('lmdA');
        customerBtn.classList.add('lmd');
        }

        await sleep(300);
        listenerIsFree = true;
    }

}