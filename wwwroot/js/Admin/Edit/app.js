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
            },
            parsedText() {
                return this.$store.state.postVersion.rawText;
            },
            tab() {
                return this.$store.state.tab;
            }
        },
        methods: {
            setTab: function (value) {
                this.$store.commit('setTab', value);
            },
        },
        mounted: function () {
            this.$nextTick(function () {
                this.$store.dispatch('init');
                document.querySelectorAll('code').forEach((block) => {
                    hljs.highlightBlock(block)
                })
            });
        },
        updated: function () {
            this.$nextTick(function () {
                document.querySelectorAll('pre code').forEach((block) => {
                    hljs.highlightBlock(block);
                });
            })
        }
    });
});