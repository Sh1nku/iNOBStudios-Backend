$(document).ready(function () {
    new Vue({
        el: '#main-container',
        store: store,
        computed: {
            post() {
                return this.$store.state.post;
            },
            postVersion() {
                return this.$store.state.postVersion;
            }
        },
        methods: {
        },
        mounted: function () {
            this.$nextTick(function () {
                this.$store.dispatch('init');
            });
        }
    });
});