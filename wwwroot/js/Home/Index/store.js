var store = new Vuex.Store({
    strict: true,
    state: {
        posts: null,
        offset: null,
        limit: null
    },
    mutations: {
        setModel: (state, value) => {
            state.posts = value.posts;
            state.limit = value.limit;
            state.offset = value.offset
        },
    },
    actions: {
        init: (context) => {
            context.commit('setModel', model);
        },
    }
});