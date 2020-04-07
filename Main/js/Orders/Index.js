const app = new Vue({

    el: "#app",
    data: {
        listProduct: null,
        
        product: {
            name: null,
        },
    },
    methods: {
        onSerche: function () {
           
            this.listProduct = null;
            axios.post('/order/GetProduct', this.product)

                .then(response => {

                    this.listProduct = response.data

                })
                .catch(error => {

                    console.error(error)

                })
                .finally(() => {

                })
        }
    },
    mounted() {
        axios.post('/order/GetProduct')

            .then(response => {

                this.listProduct = response.data

            })
            .catch(error => {

                console.error(error)

            })
            .finally(() => {

            })

         
    }

})
