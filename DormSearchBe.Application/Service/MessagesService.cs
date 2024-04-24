using AutoMapper;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Message;
using DormSearchBe.Domain.Dto.Messages;
using DormSearchBe.Domain.Entity;
using DormSearchBe.Domain.Repositories;
using DormSearchBe.Infrastructure.Exceptions;
using DormSearchBe.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.Service
{
    public class MessagesService :IMessagesService
    {
        private readonly IMessagesRepository _messagesRepository;
        private readonly IMapper _mapper;
        public MessagesService(IMessagesRepository messageRepository, IMapper mapper)
        {
            _messagesRepository = messageRepository;
            _mapper = mapper;
        }


        public DataResponse<MessageQuery> GetById(Guid id)
        {
            var item = _messagesRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            return new DataResponse<MessageQuery>(_mapper.Map<MessageQuery>(item), HttpStatusCode.OK, HttpStatusMessages.OK);
        }

        public PagedDataResponse<MessageQuery> Items(CommonListQuery commonList)
        {
            var query = _mapper.Map<List<MessageQuery>>(_messagesRepository.GetAllData());
            if (!string.IsNullOrEmpty(commonList.keyword))
            {
                query = query.Where(x => x.Message.Contains(commonList.keyword)).ToList();
            }
            var paginatedResult = PaginatedList<MessageQuery>.ToPageList(query, commonList.page, commonList.limit);
            return new PagedDataResponse<MessageQuery>(paginatedResult, 200, query.Count());
        }

        public DataResponse<List<MessageDto>> ItemsList()
        {
            var query = _messagesRepository.GetAllData();
            if (query != null && query.Any())
            {
                var MessageDtos = _mapper.Map<List<MessageDto>>(query);
                return new DataResponse<List<MessageDto>>(_mapper.Map<List<MessageDto>>(query), HttpStatusCode.OK, HttpStatusMessages.OK);
            }
            throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
        }


    }
}
