﻿using AnbarDomain.Partys;
using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AnbarService
{
    [Service]
    public class PartyManagement : IPartyManagement
    {
        private readonly IPartyRepository _partyRepository;

        public PartyManagement(IPartyRepository partyRepository)
        {
            _partyRepository = partyRepository;
        }

        public async Task<warhouses> GetPartyDataSetAsync()
        {
            return await _partyRepository.GetDataSetAsync();
        }

        public async Task AddParty(Party party1)
        {
            if (string.IsNullOrWhiteSpace(party1.Name))
                throw new ValidationException("name is empty", "username is empty", ErrorCode.UserNameEmpty);

            if (string.IsNullOrWhiteSpace(party1.Email))
                throw new ValidationException("Email is empty", "email is empty ", ErrorCode.EmailEmpty);


            await _partyRepository.InsertAsync(party1);
        }

        public async Task SaveAllChanges(DataTable partiesTable)
        {

            await _partyRepository.SaveChangesFromDataTable(partiesTable);
        }


        public async Task<List<Party>> GetAllParties()
        {
            var a = await _partyRepository.GetAllAsync();
            return a.ToList();
        }

        public async Task UpdateParty(Party party1)
        {
            if (string.IsNullOrWhiteSpace(party1.Name))
                throw new ValidationException("name is empty", "username is empty", ErrorCode.UserNameEmpty);

            if (string.IsNullOrWhiteSpace(party1.Email))
                throw new ValidationException("Email is empty", "email is empty ", ErrorCode.EmailEmpty);

            var party = await _partyRepository.GetByIdAsync(party1.Id);
            if (party == null)
                throw new ValidationException("The party Shown as created but not in db",
                    "Party Not Found", ErrorCode.PartyNotFound);


            await _partyRepository.UpdateAsync(party1, "Id");

        }

        public async Task DeleteParty(int id)
        {
            try
            {
                await _partyRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, "the user cant be deleted", ErrorCode.Internal);
            }
        }

        public async Task<Party> GetPartyById(int id)
        {
            try
            {
                return await _partyRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, "the user cant be get", ErrorCode.Internal);
            }
        }

    }
}
