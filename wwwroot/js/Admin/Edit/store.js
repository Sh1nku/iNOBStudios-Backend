var store = new Vuex.Store({
    strict: true,
    state: {
        post: null,
        postVersion: null,
        tab: 'edit'
    },
    mutations: {
        setModel: (state, value) => {
            state.post = value.post;
            state.postVersion = value.postVersion;
        },
        setTab: (state, value) => {
            state.tab = value;
        }
    },
    actions: {
        init: (context) => {
            context.commit('setModel', model);
        },
    }
});