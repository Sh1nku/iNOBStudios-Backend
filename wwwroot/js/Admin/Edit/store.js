var store = new Vuex.Store({
    strict: true,
    state: {
        post: null,
        postVersion: null
    },
    mutations: {
        setModel: (state, value) => {
            state.post = value.post;
            state.postVersion = value.postVersion;
        }
    },
    actions: {
        init: (context) => {
            context.commit('setModel', model);
        },
    }
});