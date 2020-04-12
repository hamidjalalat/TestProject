var app = new Vue({

    el: "#app",
    data: {
        selectedGroupProductId: null,
        listProduct: null,
        groupList:null,
    }, 
    methods: {
        toggleDisplayDetails: function (item) {

            item.displayDetails = !item.displayDetails

        },
    },
    computed: {
        //getUniqueGroupProductList: function () {

        //    const groupProducs = [];

        //    this.groupList.forEach(item => {

        //        if (groupProducs.includes(item.Name) == false) {

        //            groupProducs.push(item.Name)

        //        }

        //    })

        //    return groupProducs

        //},
    },

    created: function () {
        ////alert(" ffff");
        //for (let index = 0; index < this.listProduct.length; index++) {
        //    alert(this.listProduct[index]);
        //    Vue.set(this.listProduct[index], `displayDetails`, true)

        //}
     
    },
      mounted() {
     
          axios.post('/Order/GetListProduct')

              .then(response => {

                  this.listProduct = response.data.listProduct;
                  this.groupList = response.data.listGruopProduct;
                
                  for (let index = 0; index < this.listProduct.length; index++) {
                      Vue.set(this.listProduct[index], `displayDetails`, true)

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
