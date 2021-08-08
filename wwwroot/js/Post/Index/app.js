var vueReference = null;

function addReference(value) {
    vueReference.$store.commit('addReference', value);
}

function clearReferences() {
    vueReference.$store.commit('clearReferences');
}

$(document).ready(function () {
    vueReference = new Vue({
        el: '#main-container',
        store: store,
        computed: {
            post() {
                return this.$store.state.post;
            },
            references() {
                return _.orderBy(this.$store.state.references, 'count', 'asc');
            },
            parsedText() {
                return parsePost(this.$store.state.post.currentVersion.rawText);
            }
        },
        data: {

        },
        methods: {

        },
        mounted: function () {
            this.$nextTick(function () {
                this.$store.dispatch('init');
                document.querySelectorAll('pre code').forEach((block) => {
                    console.log(block);
                    hljs.highlightElement(block)
                });
            });
        },
        updated: function () {
            this.$nextTick(function () {
                document.querySelectorAll('pre code').forEach((block) => {
                    if (!block.classList.contains('hljs')) {
                        hljs.highlightElement(block);
                    }
                });
            });
        }
    });
});