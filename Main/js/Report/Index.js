const app = new Vue({

    el: "#app",
    data: {
        selectGroupId: null,
        groupList: null,
      
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
            axios.post('/Report/GetFactor', this.factor)

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
    computed: {

    },
    mounted() {

        this.onSerche();
    }

})
