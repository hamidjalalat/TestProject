
var app = new Vue({

    el: "#app",
    data: {
        selectedGroupProductId: null,
        listProduct: null,
        groupList: null,
        selectionProduct:[],
    }, 
    methods: {
        redirectToAction() {
            //$.post('/RegisterOrder/Check', { jsonOrder: JSON.stringify(this.selectionProduct) });
            window.location.href = "/RegisterOrder/Check?jsonOrder=" + encodeURIComponent(JSON.stringify(this.selectionProduct));
        },

        getAddProduct: function (item) {
            let has = true;
            this.selectionProduct.forEach(i=> {
         
                if (i.Id == item.Id) {
            
                    has = false;
                    i.count++;
                }
             
            });
            if (has) {
        
                this.selectionProduct.push(item);
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
            else
            {
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

            return separate( total)

        },

    },
    computed: {
   
   
    },

    created: function () {
        let jsonlistProduct = getCookie('listProduct');
        if (jsonlistProduct != null) {
            this.selectionProduct = JSON.parse(jsonlistProduct);
        }
     
    },
      mounted() {
     
          axios.post('/Order/GetListProduct')
              .then(response => {

                  this.listProduct = response.data.listProduct;
                  this.groupList = response.data.listGruopProduct;
                
                  for (let index = 0; index < this.listProduct.length; index++) {
                      Vue.set(this.listProduct[index], `displayDetails`, true);
                      Vue.set(this.listProduct[index], `count`, 1)

                  }
              })
              .catch(error => {

                  console.error(error)

              })
              .finally(() => {

              })
          console.log(this.listProduct)
    }
})
