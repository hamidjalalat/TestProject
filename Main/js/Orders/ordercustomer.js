$(document).ready(function () {
    $(`#order`).modal();
});

Vue.filter("formatNumber", function (value) {
    return separate(value);
});

var app = new Vue({

    el: "#app",
    data: {
        config: { maxenable: 0, maxorder: 0, maxvalue: 0},
        costPeak: false,
        showproductprop: null,
        isFlippedCssClass: `isFlipped`,

        selectedGroupProductId: null,
        listProduct: null,
        groupList: null,
        selectionProduct: [],
        description: null,
    },
    methods: {
        showproduct(item) {
            this.showproductprop = item;
            $(`#showproduct`).modal();
        },



        redirectToAction() {
            if (this.selectionProduct.length != 0) {
                $('#checkfinal').hide();
                $(`div#loadingModal`).modal({ backdrop: false, keyboard: false, });
                let parameter = { jsonOrder: JSON.stringify(this.selectionProduct), description: this.description };
                axios.post('/RegisterOrder/FirstCheck', parameter)
                 .then(response => {

                     if (response.data) {
                         window.location.href = "/RegisterOrder/SecondCheck";
                     }
                     else {
                         $(`div#errormodal`).modal();
                     }

                 })
         .catch(error => {

             $(`div#errormodal`).modal();
         })
         .finally(() => {
             $('#checkfinal').show();
             $(`div#loadingModal`).modal(`hide`)
         })
            }
            else {
                $(`div#messagemodal`).modal();
            }

        },

        getAddProduct: function (item) {

            item.isFlipped = !item.isFlipped;

            let itemGlobal = { Id: item.Id, Name: item.Name, Price: item.Price, count: item.count, hasBread: item.hasBread, Image_url: item.Image_url, Description: item.Description };

            if (itemGlobal.hasBread == true) {
                itemGlobal.Price += parseInt(this.config.breadPrice);
                itemGlobal.Name += "  با نان اضافه  "
            }


            let has = true;
            this.selectionProduct.forEach(i => {

                if (i.Id == itemGlobal.Id && itemGlobal.hasBread == i.hasBread) {
                    has = false;
                    i.count++;
                }

            });
            if (has) {

                this.selectionProduct.push(itemGlobal);

            }


            document.cookie = "listProduct=" + JSON.stringify(this.selectionProduct);
        },

        plus: function (item) {
            item.count++;

            document.cookie = "listProduct=" + JSON.stringify(this.selectionProduct);
        },

        minus: function (item) {

            if (item.count > 1) {

                item.count--

            }
            else {
                const index = this.selectionProduct.indexOf(item);
                if (index > -1) {
                    this.selectionProduct.splice(index, 1);
                }
            }

            document.cookie = "listProduct=" + JSON.stringify(this.selectionProduct);

        },

        getSubTotal: function (item) {
            let result =
                item.count * item.Price

            return result
        },

        getTotal: function () {

            let total = 0

            for (let index = 0; index < this.selectionProduct.length; index++) {

                let currentItem = this.selectionProduct[index]

                total +=
                    this.getSubTotal(currentItem)

            }
            this.selectionProduct.Total = total;
            return total

        },
     


    },
    computed: {
        getTotalFinished: function () {
            var result = this.getTotal();

            if ((this.getTotal() < this.config.maxorder) && (this.config.maxenable == "1")) {
                result = (parseInt(this.getTotal()) + parseInt(this.config.maxvalue));
                this.costPeak = true;
            } else {
                this.costPeak = false;
            }
            if (this.getTotal() == 0) {
                this.costPeak = false;
                return 0;
            }


            return separate(result);
        }

    },


    created: function () {
         axios.post('/Order/GetConfig')
          .then(response => {

           this.config = response.data.config;
          })

        let jsonlistProduct = getCookie('listProduct');
   
        try {
            this.selectionProduct = JSON.parse(jsonlistProduct);
        } catch (e) {
            this.selectionProduct = [];
        }

    },


    mounted() {
        axios.post('/Order/GetListProduct')
    .then(response => {

        this.config = response.data.config;

        this.listProduct = response.data.listProduct;
        this.groupList = response.data.listGruopProduct;

        for (let index = 0; index < this.listProduct.length; index++) {
            Vue.set(this.listProduct[index], `hasBread`, false)
            Vue.set(this.listProduct[index], `count`, 1)

        }
    })
     


    },


    


})
