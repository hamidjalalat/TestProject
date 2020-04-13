var app = new Vue({

    el: "#app",
    data: {
        selectedGroupProductId: null,
        listProduct: null,
        groupList: null,
        selectionProduct:[],
    }, 
    methods: {
        getAddProduct: function (item) {
          

            this.selectionProduct.push(item);
        },




        plus: function (item) {

            if (item.count < 10) {

                item.count++

            }

        },

        minus: function (item) {

            if (item.count > 0) {

                item.count--

            }

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

            return total

        },

    },
    computed: {
   
   
    },

    created: function () {
     
     
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
