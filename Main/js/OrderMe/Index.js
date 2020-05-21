
const app = new Vue({

    el: "#app",
    data: {
        config: { maxenable: 0, maxorder: 0, maxvalue: 0 },
        
        selectGroupId: null,
        groupList: null,
        detailsFactor:[],
        id: {
            idDelete: 0,
            idEdit: 0,

        },
        listFactor: null,
        labelPageIndex: null,
        labelPageCount: null,

        count: null,
        factor: {
            pageSize: 10,
            pageIndex: 0,
            userName: null,
            address: null,
            mobile: null,
        },
    },
    methods: {
        details(id) {
            let parameter = { id:id };
            axios.post('/OrderMe/GetDatialFactor', parameter)
                .then(response => {
                    this.detailsFactor = response.data;
                })
            $(`div#details`).modal();
        },
        getSubTotal: function (item) {
            let result =
                item.count * item.Price

            return result
        },

        getTotal: function () {

            let total = 0

            for (let index = 0; index < this.detailsFactor.length; index++) {

                let currentItem = this.detailsFactor[index]

                total +=
                    this.getSubTotal(currentItem)

            }
            this.detailsFactor.Total = total;
          
            return total

        },
     


        GetLastPageIndex: function () {

            let intCount = this.count;

            if ((parseInt(intCount) % parseInt(this.factor.pageSize)) == 0) {

                return ((parseInt(intCount) / parseInt(this.factor.pageSize)) - 1);
            }
            else {
                return Math.floor((parseInt(intCount) / parseInt(this.factor.pageSize)));
            }
        },

        firstButtonClick: function () {

            this.factor.pageIndex = 0;
            this.onSerche();

        },
        previousButtonClick: function () {

            if (this.factor.pageIndex > 0) {
                this.factor.pageIndex--;
                this.onSerche();
            }

        },

        nextButtonClick: function () {

            if (this.factor.pageIndex < (this.GetLastPageIndex())) {
                this.factor.pageIndex++;
                this.onSerche();
            }

        },
        lastButtonClick: function () {

            this.factor.pageIndex = this.GetLastPageIndex();
            this.onSerche();

        },
        setParameter: function () {

            this.factor.pageSize = 10;
            this.factor.pageIndex = 0;

        },
        onSerche: function () {

            this.listFactor = null;
            axios.post('/OrderMe/GetFactor', this.factor)

                .then(response => {

                    this.listFactor = response.data.data;
                    this.count = response.data.count;
                    this.labelPageIndex = (parseInt(this.factor.pageIndex) + 1);
                    this.labelPageCount = (parseInt(this.GetLastPageIndex()) + 1);

                })
                .catch(error => {

                    console.error(error)

                })
                .finally(() => {

                })
        }

    },
    created: function () {
        axios.post('/Order/GetConfig')
         .then(response => {

             this.config = response.data.config;
         })
    },

    computed: {
        costpeack() {

            let total = 0

            for (let index = 0; index < this.detailsFactor.length; index++) {

                let currentItem = this.detailsFactor[index]

                total +=
                    this.getSubTotal(currentItem)

            }
            if (total < this.config.maxorder) {
                return true;
            }else
            {
                return false;
            }
           
        },
        getTotalFinished: function () {
            var result = this.getTotal();

            if ((this.getTotal() < this.config.maxorder) && (this.config.maxenable == "1")) {
                result = (parseInt(this.getTotal()) + parseInt(this.config.maxvalue));
            } else {
            }
            if (this.getTotal() == 0) {
                return 0;
            }


            return separate(result);
        },
     
    },
    mounted() {

        this.onSerche();
    }

})

